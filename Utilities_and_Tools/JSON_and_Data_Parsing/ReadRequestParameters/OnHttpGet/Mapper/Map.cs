using AutoMapper;
using OnHttpGet.Models;

namespace OnHttpGet.Mapper;

public class Map : Profile
{
    public Map()
    {
        CreateMap<Product, ProductDto>();
    }
}