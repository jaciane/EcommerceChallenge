using Catalog.GRPC.Entities;

namespace Catalog.GRPC.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }

}
