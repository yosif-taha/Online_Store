using Online_Store.Domain.Entites.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Contracts
{
    public interface IBasketReposatory
    {
       public Task<CustomerBasket?> GetBasketAsync(string id);
       public Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket , TimeSpan duration);
       public Task<bool> DeleteBasketAsync(string id);
    }
}
