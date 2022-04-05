namespace Catalog.GRPC.Entities
{
    public class ProductsResponse
    {
        public IEnumerable<Entities.Product> Products { get; set; } = new List<Entities.Product>();
    }
}
