using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class BasketItem
    {
        [Display(Name = "id")]
        public int IdProduct { get; set; }

        [Display(Name = "quantity")]
        public int Quantity { get; set; }

        [Display(Name = "unit_amount")]
        public decimal Amount { get; set; }

        [Display(Name = "total_amount")]
        public decimal TotalAmountProduct { get; set; }

        [Display(Name = "total_discount")]
        public decimal TotalDiscountProduct { get; set; }

        [Display(Name = "is_gift")]
        public bool IsGift { get; set; }
    }
}
