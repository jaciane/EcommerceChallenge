using Basket.API.Entities;
namespace Basket.API.Interfaces
{
    public interface IBasketService
    {
        Task<BasketCheckout> GetBasketAsync(List<BasketItemRequest> ItemsRequest);
    }
}
