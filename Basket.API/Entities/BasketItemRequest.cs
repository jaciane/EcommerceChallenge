using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class BasketItemRequest
    {
        [Display(Name = "id")]
        public int ProductId { get; set; }

        [Display(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
