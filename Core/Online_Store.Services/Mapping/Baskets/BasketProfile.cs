using AutoMapper;
using Online_Store.Domain.Entites.Basket;
using Online_Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
           CreateMap<BasketItems,BasketItemsDto>().ReverseMap();
           CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();

        }
    }
}
