using AutoMapper;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Products;
using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Services.Abstractions.Product;
using Online_Store.Services.Specifications;
using Online_Store.Services.Specifications.Products;
using Online_Store.Shared;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Product
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters parameters)
        {

            var spec = new ProductsWithBrandAndTypeSpecifications(parameters);
            
           var product = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().GetAllAsync(spec);
           var result = _mapper.Map<IEnumerable<ProductResponse>>(product);



            var specCount =new ProductsCountSpecifications(parameters);

            var count = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().CountAsync(specCount);

            return new PaginationResponse<ProductResponse>(parameters.PageIndex,parameters.PageSize, count, result);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().GetAsync(spec);

            if (product == null) throw new ProductNotFoundException(id);
            var result =  _mapper.Map<ProductResponse>(product);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandtAsync()
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }
        

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypeAsync()
        {
            var Types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);
            return result;
        }

    }
}
