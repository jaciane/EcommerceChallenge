using Basket.API.Entities;

namespace Basket.API.Interfaces
{
    public interface IMatch
    {
        bool IsMatch(IEnumerable<BasketItem> products);
    }
}
