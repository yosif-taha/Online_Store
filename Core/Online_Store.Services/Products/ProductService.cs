using AutoMapper;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Products;
using Online_Store.Services.Abstractions.Product;
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
        public async Task<IEnumerable<ProductResponse>> GetAllProductAsync()
        {

           var product = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().GetAllAsync();
           var result = _mapper.Map<IEnumerable<ProductResponse>>(product);
            return result;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().GetAsync(id);
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
