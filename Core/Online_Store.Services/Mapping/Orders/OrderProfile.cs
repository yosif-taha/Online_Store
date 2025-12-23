using AutoMapper;
using Online_Store.Domain.Entites.Orders;
using Online_Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.productName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictrulUrl, O => O.MapFrom(S => S.Product.PictureUrl))
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId));

            CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.Total, O => O.MapFrom(S => S.Total()));

            CreateMap<DeliveryMethod,DeliveryMethodDto>();
        }
    }
}
