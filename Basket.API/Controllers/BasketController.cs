using Microsoft.AspNetCore.Mvc;
using Basket.API.Interfaces;
using Basket.API.Entities;
using MassTransit;

namespace Basket.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        protected readonly IBasketService _basketService;
       
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
           
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCheckoutResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketCheckoutResponse>> Checkout(BasketCheckoutRequest request)
        {
            try
            {
                var products = await _basketService.GetProductsAsync();
                return Ok( await _basketService.GetBasketAsync(request.Products, products));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
