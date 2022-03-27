namespace Basket.API.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class Products
    {
        [Display(Name = "id")]
        public int IdProduct { get; set; }

        [Display(Name = "title")]
        public string? Title { get; set; }

        [Display(Name = "description")]
        public string? Description { get; set; }

        [Display(Name = "amount")]
        public decimal Amount { get; set; }

        [Display(Name = "is_gift")]
        public bool Is_gift { get; set; }
    }
}
