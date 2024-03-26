using AutoMapper;
using ProductsAPI.Models;

namespace SharedLib;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
    }
}
