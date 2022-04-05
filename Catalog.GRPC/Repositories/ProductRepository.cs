using Catalog.GRPC.Entities;
using Catalog.GRPC.Interfaces;
using MongoDB.Driver;

namespace Catalog.GRPC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProducts()
        {
            return await _context.Products.Find(p=>p.IdProduct>0).ToListAsync();
        }

    }
}
