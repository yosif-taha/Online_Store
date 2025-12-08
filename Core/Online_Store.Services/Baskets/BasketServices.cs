using AutoMapper;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Basket;
using Online_Store.Domain.Exeptions.BadRequest;
using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Services.Abstractions.Baskets;
using Online_Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Baskets
{
    public class BasketServices(IBasketReposatory _basketReposatory , IMapper _mapper) : IBasketServices
    {
        public async Task<CustomerBasketDto?> GetBasketAsync(string id)
        {
          var basket = await _basketReposatory.GetBasketAsync(id);
            if (basket == null) throw new BasetNotFoungException(id);
           var result = _mapper.Map<CustomerBasketDto>(basket);
            return result;
        }
        public async Task<CustomerBasketDto?> CreateBasketAsync(CustomerBasketDto dto, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(dto);
          var result = await  _basketReposatory.CreateBasketAsync(basket,duration);
            if (result is null) throw new CreateOrUpdateBadRequestException();
           return _mapper.Map<CustomerBasketDto>(result);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
          var flage = await _basketReposatory.DeleteBasketAsync(id);
            if (!flage) throw new DeleteBadRequestException();
            return flage;
        }

    }
}
