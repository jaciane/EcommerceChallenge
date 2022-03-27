using Basket.API.Entities;
using Basket.API.Interfaces;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly Discount.Discount.DiscountClient _discountClient;
        private DateTime _blackFriday = Convert.ToDateTime("25/11/2022");
        private BasketCheckout basketCheckout { get; set; } = new();

        public BasketService(Discount.Discount.DiscountClient discountClient)
        {
            _discountClient = discountClient;
        }

        /// <summary>
        /// Get basket with Calculated values 
        /// </summary>
        /// <param name="ItemsRequest">Basket items request</param>
        /// <returns>Basket</returns>
        /// <exception cref="Exception"></exception>
        public async Task<BasketCheckout> GetBasketAsync(List<BasketItemRequest> ItemsRequest)
        {
            if(ItemsRequest == null)
                throw new ArgumentNullException(nameof(ItemsRequest));

            decimal totalAmount = 0;
            decimal totalDiscount = 0;
            List<Products> products = new List<Products>(); //TODO: change
            List<BasketItem> BasketItems = new List<BasketItem>();

            foreach (var item in ItemsRequest)
            {
                if (!IsMatch(products, item.ProductId))
                    throw new Exception("Produto não existe!"); //TODO: tratar isso

                var discount = await GetDiscountAsync(item.ProductId);

                BasketItem basketItem = new BasketItem();
                basketItem.IdProduct = item.ProductId;
                basketItem.Quantity = item.Quantity;
                var product = products.Select(p => p).Where(p => p.IdProduct == item.ProductId).FirstOrDefault();
                basketItem.Amount = product.Amount;
                basketItem.TotalAmountProduct = basketItem.Quantity * basketItem.Amount;
                basketItem.TotalDiscountProduct = discount > 0 ? (product.Amount * item.Quantity) * discount : 0;
                basketItem.IsGift = product.Is_gift;

                totalDiscount += basketItem.TotalDiscountProduct;
                totalAmount += basketItem.Amount;
                basketCheckout.Products.Add(basketItem);
            }

            basketCheckout.TotalAmount = totalAmount;
            basketCheckout.TotalDiscount = totalDiscount;
            basketCheckout.TotalAmountWithDiscount = basketCheckout.TotalAmount - basketCheckout.TotalDiscount;
            
            //Add the gift
            if (IsBlackFriday() && !ContainsGift(basketCheckout.Products))
                AddGift(GetBasketItemGift(products.Select(p => p).Where(p => p.Is_gift == true).FirstOrDefault()));

            return basketCheckout;
        }

        /// <summary>
        /// Get a gift item
        /// </summary>
        /// <param name="product">A product which is gift</param>
        /// <returns>Gift item</returns>
        private BasketItem GetBasketItemGift(Products product)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.IdProduct = product.IdProduct;
            basketItem.Quantity = 1;
            basketItem.Amount = 0;
            basketItem.TotalAmountProduct = 0;
            basketItem.TotalDiscountProduct = 0;
            basketItem.IsGift = true;
            return basketItem;

        }

        /// <summary>
        /// Product discount percentage
        /// </summary>
        /// <param name="producId">Product Id</param>
        /// <returns>Product discount percentage</returns>
        private async Task<decimal> GetDiscountAsync(int producId)
        {
            try
            {
                Discount.GetDiscountResponse discount = await _discountClient.GetDiscountAsync(new Discount.GetDiscountRequest { ProductID = producId });

                return (decimal)discount.Percentage;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Check if the requested product exists in the database
        /// </summary>
        /// <param name="products">Products list of database</param>
        /// <param name="productId">Identifier of product to check</param>
        /// <returns>True or false</returns>
        private bool IsMatch(IEnumerable<Products> products, int productId)
        {
            return products.Any(p => p.IdProduct == productId);
        }

        /// <summary>
        /// Check if current day is black friday
        /// </summary>
        /// <returns> True or false</returns>
        private bool IsBlackFriday()
        {
            return DateTime.Compare(DateTime.Now, _blackFriday) == 0 ? true: false;
        }

        /// <summary>
        /// Add Gift Item to Basket
        /// </summary>
        /// <param name="gift">Gift Item</param>
        private void AddGift(BasketItem gift)
        {
            basketCheckout.Products.Add(gift);
        }

        /// <summary>
        /// Check there is any gift item into basket
        /// </summary>
        /// <param name="products">BasketItem list</param>
        /// <returns> True ou false</returns>
        private bool ContainsGift(IEnumerable<BasketItem> products)
        {
            return products.Any(p => p.IsGift == true);
        }

    }
}
