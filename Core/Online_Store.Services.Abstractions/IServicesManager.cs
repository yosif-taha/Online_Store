using Online_Store.Services.Abstractions.Baskets;
using Online_Store.Services.Abstractions.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions
{
    public interface IServicesManager
    {
       IProductService ProductService { get; }
       IBasketServices BasketService { get; }
    }
}
