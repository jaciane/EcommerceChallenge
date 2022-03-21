using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Repositories
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
            return await _context.Products.Find(p=>true).ToListAsync();
        }

    }
}
