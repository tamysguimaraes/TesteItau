using AutoMapper;
using Products.Data.Entities;
using Products.Domain.Models;

namespace Products.Domain.Mapping
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<ProductEntity, ProductCreate>().ReverseMap();
            CreateMap<ProductCreate, ProductEntity>().ReverseMap();
            CreateMap<ProductEntity, ProductReturn>().ReverseMap();

        }
    }
}
