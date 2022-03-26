using System.ComponentModel.DataAnnotations;
using Basket.API.Interfaces;

namespace Basket.API.Entities
{
    public class BasketCheckout : IMatch  // add interface
    {
        [Display(Name = "total_amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "total_amount_with_discount")]
        public decimal TotalAmountWithDiscount { get; set; }

        [Display(Name = "total_discount")]
        public decimal TotalDiscount { get; set; }

        [Display(Name = "products")]
        List<BasketItem> Products { get; set; } = new();

        public BasketCheckout()
        {

        }

        public void CalcTotalProductValue()
        {

        }

        public decimal CalcTotalPurchaseAmount(List<BasketItem> items)
        {
            decimal total = 0;
            foreach (BasketItem item in items)
            {
                total +=  item.Quantity * item.Amount;
            }

            return total;
        }

        public bool IsValid()
        {
            //check if is black friday
            //validate is gift and how many exists
            // check request product IMatch 
            return true;
        }

        public bool IsMatch(IEnumerable<BasketItem> products)
        {
            return (products
                .Where(p => p.IdProduct == 2) //TODO: change value
                .GroupBy(c => c.IdProduct)
                .Select(g => new
                {
                    Quantity = g.Sum(e => e.Quantity),
                })).FirstOrDefault()?.Quantity >= 1 ? true : false;
        }



    }
}
