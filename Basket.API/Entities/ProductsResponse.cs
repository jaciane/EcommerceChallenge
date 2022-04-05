namespace Basket.API.Entities
{
    public class ProductsResponse
    {
        public IEnumerable<Entities.Product> Products { get; set; } = Enumerable.Empty<Entities.Product>();
    }
}
