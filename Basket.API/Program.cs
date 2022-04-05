using Basket.API.Interfaces;
using Basket.API.Mapper;
using Basket.API.Protos;
using Basket.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region GRPC configurations
builder.Services.AddScoped<IBasketService, BasketService>();

//gRPC Discount
builder.Services.AddGrpcClient<Discount.DiscountClient>(options=>
        options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountURL")));

//gRPC Catalog
builder.Services.AddGrpcClient<Products.ProductsClient>(options =>
        options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcCatalogSettings:CatalogURL")));

builder.Services.AddAutoMapper(typeof(Map));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
