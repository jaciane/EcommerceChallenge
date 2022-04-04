using Basket.API.Entities;

namespace Basket.API.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCheckout> GetBasket();
        Task<BasketCheckout> UpdateBasket(BasketCheckout basket);
        Task<BasketCheckout> DeleteBasket();

    }
}
