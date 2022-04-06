using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class BasketItemRequest
    {
        [Required(ErrorMessage = "Identificador do produto deve ser informado!")]
        [Display(Name = "id", Description = "Informe um inteiro entre 1 e 6.")]
        //[Range(1, 99999)]   
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantidade deve ser informada!")]
        [Display(Name = "quantity")]
        [Range(1, 99999)]
        public int Quantity { get; set; }
    }
}
