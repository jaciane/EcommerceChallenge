using Basket.API.Entities;
namespace Basket.API.Interfaces
{
    public interface IBasketService
    {
        Task<BasketCheckoutResponse> GetBasketAsync(List<BasketItemRequest> ItemsRequest, IEnumerable<Entities.Product> productList);
        bool IsMatch(IEnumerable<Basket.API.Entities.Product> products, int productId);
        Task<IEnumerable<Entities.Product>> GetProductsAsync();
        bool IsBlackFriday(DateTime blackFriday);
    }
}
