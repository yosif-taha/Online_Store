using AutoMapper;
using Online_Store.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store.Shared.Dtos;

namespace Online_Store.Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Online_Store.Domain.Entites.Products.Product, ProductResponse>()
                .ForMember(D => D.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(D => D.Type, O => O.MapFrom(s => s.Type.Name));  // this step to equality of (Brand , Type) DataTypes in two different classes 'ProductResponse , Product'

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();

        }
    }
}
