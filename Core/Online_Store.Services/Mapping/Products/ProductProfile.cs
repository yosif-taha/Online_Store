using AutoMapper;
using Online_Store.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store.Shared.Dtos;
using Microsoft.Extensions.Configuration;

namespace Online_Store.Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Online_Store.Domain.Entites.Products.Product, ProductResponse>()
                .ForMember(D => D.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(D => D.Type, O => O.MapFrom(s => s.Type.Name))  // this step to equality of (Brand , Type) DataTypes in two different classes 'ProductResponse , Product'
             .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductPictureUrlResolver(configuration))); //Use second overloding of this funcion, TO send complete of Url ,concat between BaseUrl in Appsetting and PictureUrl in DB , Use 'configuration' because BaseUrl is Constant Saved in appsettings.
            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();

        }
    }
}
