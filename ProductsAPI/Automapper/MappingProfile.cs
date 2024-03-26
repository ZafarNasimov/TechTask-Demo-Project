using AutoMapper;
using ProductsAPI.Models;

namespace ProductsAPI.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
    }
}
