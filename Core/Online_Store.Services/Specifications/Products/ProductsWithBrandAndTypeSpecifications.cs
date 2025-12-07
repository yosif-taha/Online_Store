using Online_Store.Domain.Entites.Products;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Specifications.Products
{

    // this class to use second oveload from genericrepository functions in productService class, this special case from 'BaseSpecifications' for product. 
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Online_Store.Domain.Entites.Products.Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {

            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            ApllySorting(parameters.Sort);

            ApplyIncludes();
        }


        private void ApllySorting(string? sort)
        {
            //priceasc
            //pricedesc
            //nameasc

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //OrderByDescending = P => P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        //OrderBy = P => P.Name;
                        AddOrderBy(P => P.Name);
                        break;

                }

            }
            else
            {
                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
            }
        }
        private void ApplyIncludes()
        {

            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
