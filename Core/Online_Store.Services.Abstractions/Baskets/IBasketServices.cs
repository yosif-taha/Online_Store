using Online_Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions.Baskets
{
    public interface IBasketServices
    {
       Task<CustomerBasketDto?> GetBasketAsync(string id);
        Task<CustomerBasketDto?> CreateBasketAsync(CustomerBasketDto dto ,TimeSpan duration);
        Task<bool> DeleteBasketAsync(string id);
    }
}
