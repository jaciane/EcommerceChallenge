using Basket.API.Entities;

namespace Basket.API.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCheckoutResponse> GetBasket();
        Task<BasketCheckoutResponse> UpdateBasket(BasketCheckoutResponse basket);
        Task<BasketCheckoutResponse> DeleteBasket();

    }
}
