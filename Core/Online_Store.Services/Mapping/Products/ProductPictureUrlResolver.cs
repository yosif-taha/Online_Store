using AutoMapper;
using Microsoft.Extensions.Configuration;
using Online_Store.Domain.Entites.Products;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Mapping.Products
{
    public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Online_Store.Domain.Entites.Products.Product, ProductResponse, string>
    {
        public string Resolve(Domain.Entites.Products.Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty ;

        }
    }
}
