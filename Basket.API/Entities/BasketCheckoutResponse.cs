using System.ComponentModel.DataAnnotations;
using Basket.API.Interfaces;

namespace Basket.API.Entities
{
    public class BasketCheckoutResponse
    {
        [Display(Name = "total_amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "total_amount_with_discount")]
        public decimal TotalAmountWithDiscount { get; set; }

        [Display(Name = "total_discount")]
        public decimal TotalDiscount { get; set; }

        [Display(Name = "products")]
        public List<BasketItemResponse> Products { get; set; } = new();


    }
}
