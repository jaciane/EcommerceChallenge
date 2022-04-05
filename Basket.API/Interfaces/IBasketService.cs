using Basket.API.Entities;
namespace Basket.API.Interfaces
{
    public interface IBasketService
    {
        Task<BasketCheckoutResponse> GetBasketAsync(List<BasketItemRequest> ItemsRequest);
    }
}
