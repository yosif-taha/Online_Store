using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Identity;
using Online_Store.Services.Abstractions;
using Online_Store.Services.Abstractions.Auth;
using Online_Store.Services.Abstractions.Baskets;
using Online_Store.Services.Abstractions.Cache;
using Online_Store.Services.Abstractions.Orders;
using Online_Store.Services.Abstractions.Products;
using Online_Store.Services.Auth;
using Online_Store.Services.Baskets;
using Online_Store.Services.Cache;
using Online_Store.Services.Orders;
using Online_Store.Services.Product;
using Online_Store.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services
{
    public class ServicesManager(IUnitOfWork _unitOfWork ,
        IMapper _mapper ,
        IBasketReposatory _basketReposatory,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManager,
        IOptions<JwtOptions> _options) :
        IServicesManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketServices BasketService { get; } = new BasketServices(_basketReposatory, _mapper);

        public ICacheService CacheService { get; } = new CachService(_cacheRepository);

        public IAuthService AuthServices { get; } = new AuthServices(_userManager, _options);

        public IOrderService OrderServices { get; } = new OrderService(_unitOfWork,_mapper,_basketReposatory);
    }
}
