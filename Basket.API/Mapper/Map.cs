using AutoMapper;
using Basket.API.Entities;
using Basket.API.Protos;
using Google.Protobuf.Collections;

namespace Basket.API.Mapper
{
    public class Map: Profile
    {
        public Map()
        {
            CreateMap<GetProductsResponse, ProductsResponse>()
              .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Produtcts))
              .ReverseMap();
            CreateMap<Protos.Product, Entities.Product>()
              .ForMember(dest => dest.IdProduct, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Is_gift, opt => opt.MapFrom(src => src.IsGift))
              // .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ReverseMap();
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));
        }
    }

    class EnumerableToRepeatedFieldTypeConverter<TITemSource, TITemDest> : ITypeConverter<IEnumerable<TITemSource>, RepeatedField<TITemDest>>
    {
        public RepeatedField<TITemDest> Convert(IEnumerable<TITemSource> source, RepeatedField<TITemDest> destination, ResolutionContext context)
        {
            destination = destination ?? new RepeatedField<TITemDest>();
            if (source != null)
            {
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<TITemDest>(item));
                }
            }
            return destination;
        }
    }

    class RepeatedFieldToListTypeConverter<TITemSource, TITemDest> : ITypeConverter<RepeatedField<TITemSource>, List<TITemDest>>
    {
        public List<TITemDest> Convert(RepeatedField<TITemSource> source, List<TITemDest> destination, ResolutionContext context)
        {
            destination = destination ?? new List<TITemDest>();
            foreach (var item in source)
            {
                destination.Add(context.Mapper.Map<TITemDest>(item));
            }
            return destination;
        }
    }
}
