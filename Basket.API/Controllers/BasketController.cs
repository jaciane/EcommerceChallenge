using Microsoft.AspNetCore.Mvc;
using Basket.API.Interfaces;
using Basket.API.Entities;

namespace Basket.API.Controllers
{
    public class BasketController : Controller
    {
        protected readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<BasketCheckout> Checkout(BasketCheckoutRequest request)
        {
            return await _basketService.GetBasketAsync(request.Products);
        }
    }
}
