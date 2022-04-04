using AutoMapper;
using Basket.API.Protos;

namespace Basket.API.Mapper
{
    public class Map: Profile
    {
        public Map()
        {
            CreateMap<Basket.API.Entities.Products,GetProductsResponse>().ReverseMap();
        }
    }
}
