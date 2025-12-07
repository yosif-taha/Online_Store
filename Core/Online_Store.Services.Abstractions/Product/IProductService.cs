using Online_Store.Shared;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions.Product
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters parameters);
        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandTypeResponse>> GetAllBrandtAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypeAsync();

    }
}
