using Online_Store.Services.Abstractions.Auth;
using Online_Store.Services.Abstractions.Baskets;
using Online_Store.Services.Abstractions.Cache;
using Online_Store.Services.Abstractions.Orders;
using Online_Store.Services.Abstractions.Payment;
using Online_Store.Services.Abstractions.Products;
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
       ICacheService CacheService { get; }
       IAuthService AuthServices { get; }
       IOrderService OrderServices { get; }
       IPaymentService PaymentServices { get; }
    }
}
