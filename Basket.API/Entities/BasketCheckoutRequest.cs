using System.ComponentModel.DataAnnotations;
namespace Basket.API.Entities
{
    public class BasketCheckoutRequest
    {
        [Display(Name = "products")]
        public List<BasketItemRequest> Products { get; set; } = new();
    }
}
