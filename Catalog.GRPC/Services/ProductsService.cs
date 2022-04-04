using Catalog.GRPC;
using Catalog.GRPC.Protos;
using Grpc.Core;
using Catalog.GRPC.Entities;
using Catalog.GRPC.Interfaces;
using AutoMapper;

namespace Catalog.GRPC.Services
{
    public class ProductsService : Products.ProductsBase
    {
        private readonly ILogger<ProductsService> _logger;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductsService(ILogger<ProductsService> logger, IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<GetProductsResponse> GetProducts(global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            var products = await _repository.GetProducts();
            var response = _mapper.Map<GetProductsResponse>(new ProductsResponse { Products = products });

            return response;
        }

    }
}