using AutoMapper;
using Basket.API.Entities;
using Basket.API.Interfaces;
using Basket.API.Protos;
using Google.Protobuf.WellKnownTypes;
using System.Globalization;

namespace Basket.API.Services
{

    public class BasketService : IBasketService
    {
        private readonly Discount.DiscountClient _discountClient;
        private readonly Products.ProductsClient _productsClient;
        private readonly IMapper _mapper;
        private DateTime _blackFriday = DateTime.ParseExact("25/11/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);


        private BasketCheckoutResponse basketCheckout { get; set; } = new();

        public BasketService(Discount.DiscountClient discountClient, Products.ProductsClient productsClient, IMapper mapper)
        {
            _discountClient = discountClient ?? throw new ArgumentNullException(nameof(discountClient));
            _productsClient = productsClient ?? throw new ArgumentNullException(nameof(productsClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get basket with Calculated values 
        /// </summary>
        /// <param name="ItemsRequest">Basket items request</param>
        /// <returns>Basket</returns>
        /// <exception cref="Exception"></exception>
        public async Task<BasketCheckoutResponse> GetBasketAsync(List<BasketItemRequest> ItemsRequest, IEnumerable<Entities.Product> productList)
        {

            if (ItemsRequest == null)
                throw new ArgumentNullException(nameof(ItemsRequest));

            decimal totalAmount = 0;
            decimal totalDiscount = 0;

            IEnumerable<Basket.API.Entities.Product> products = productList; // await GetProductsAsync();
            var giftsProducts = products.Select(p => p).Where(p => p.Is_gift == true).ToList();
            products = products.Select(p => p).Where(p => p.Is_gift == false).ToList();

            foreach (var item in ItemsRequest)
            {
                if (!IsMatch(products, item.ProductId))
                    throw new Exception("Produto inválido!");

                var discount = await GetDiscountAsync(item.ProductId);

                BasketItemResponse basketItem = new BasketItemResponse();
                basketItem.IdProduct = item.ProductId;
                basketItem.Quantity = item.Quantity;
                var product = products.Select(p => p).Where(p => p.IdProduct == item.ProductId).FirstOrDefault();
                basketItem.Amount = product.Amount;
                basketItem.TotalAmountProduct = basketItem.Quantity * basketItem.Amount;
                basketItem.TotalDiscountProduct = discount > 0 ? (product.Amount * item.Quantity) * discount : 0;
                basketItem.IsGift = product.Is_gift;

                totalDiscount += basketItem.TotalDiscountProduct;
                totalAmount += basketItem.TotalAmountProduct;
                basketCheckout.Products.Add(basketItem);
            }

            basketCheckout.TotalAmount = totalAmount;
            basketCheckout.TotalDiscount = totalDiscount;
            basketCheckout.TotalAmountWithDiscount = totalAmount - totalDiscount;
            
            //If black friday, add the gift
            if (IsBlackFriday(_blackFriday) && !ContainsGift(basketCheckout.Products) && giftsProducts.Count>0)
                AddGift(GetBasketItemGift(giftsProducts?.FirstOrDefault()));

            return basketCheckout;
        }

        /// <summary>
        /// Get all products of catalog
        /// </summary>
        /// <returns>Product list</returns>
        public async Task<IEnumerable<Entities.Product>> GetProductsAsync()
        {
            try
            {
                var getProductResponse = await _productsClient.GetProductsAsync(new Google.Protobuf.WellKnownTypes.Empty(), null);
                var productResponse = _mapper.Map<ProductsResponse>(getProductResponse);
                return productResponse.Products;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Get a gift item
        /// </summary>
        /// <param name="product">A product which is gift</param>
        /// <returns>Gift item</returns>
        private BasketItemResponse GetBasketItemGift(Basket.API.Entities.Product product)
        {
            BasketItemResponse basketItem = new BasketItemResponse();
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
                var discount = await _discountClient.GetDiscountAsync(new GetDiscountRequest { ProductID = producId });
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
        public bool IsMatch(IEnumerable<Basket.API.Entities.Product> products, int productId)
        {
            return products.Any(p => p.IdProduct == productId);
        }

        /// <summary>
        /// Check if current day is black friday
        /// </summary>
        /// <returns> True or false</returns>
        public bool IsBlackFriday(DateTime blackFriday)
        {
            return DateTime.Compare(DateTime.Now.Date, blackFriday) == 0 ? true: false;
        }

        /// <summary>
        /// Add Gift Item to Basket
        /// </summary>
        /// <param name="gift">Gift Item</param>
        private void AddGift(BasketItemResponse gift)
        {
            basketCheckout.Products.Add(gift);
        }

        /// <summary>
        /// Check there is any gift item into basket
        /// </summary>
        /// <param name="products">BasketItem list</param>
        /// <returns> True ou false</returns>
        private bool ContainsGift(IEnumerable<BasketItemResponse> products)
        {
            return products.Any(p => p.IsGift == true);
        }


    }
}
