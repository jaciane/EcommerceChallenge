using Catalog.GRPC.Services;
using Catalog.GRPC.Repositories;
using Catalog.GRPC.Interfaces;
using Catalog.GRPC.Data;
using Catalog.GRPC.Mapper;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

#region depency injection 
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(Map));
#endregion


var app = builder.Build();
app.Urls.Add("http://catalog.grpc:50052");
// Configure the HTTP request pipeline.
app.MapGrpcService<ProductsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
