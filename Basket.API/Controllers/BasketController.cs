using Microsoft.AspNetCore.Mvc;
using Basket.API.Interfaces;
using Basket.API.Entities;
using MassTransit;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        protected readonly IBasketService _basketService;
       
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
           
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { Something = "A" });
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCheckoutResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketCheckoutResponse>> Checkout(BasketCheckoutRequest request)
        {
            return Ok( await _basketService.GetBasketAsync(request.Products));
        }


    }
}
