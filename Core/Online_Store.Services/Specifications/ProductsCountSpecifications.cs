using Online_Store.Domain.Entites.Products;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Specifications
{
    public class ProductsCountSpecifications : BaseSpecifications<int , Online_Store.Domain.Entites.Products.Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base
            (
                  P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            
        }
    }
}
