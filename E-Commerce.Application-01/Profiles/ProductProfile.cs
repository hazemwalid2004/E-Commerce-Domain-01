using E_Commerce.Application_01.DTOS.Products;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Application_01.Profiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Products, ProductDto>()
                .ForMember(d => d.ProductBrand, p => p.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, p => p.MapFrom(s => s.ProductType.Name)).
                ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
