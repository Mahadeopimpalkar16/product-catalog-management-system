using AutoMapper;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Entities;

namespace ProductCatalog.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : string.Empty));

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
