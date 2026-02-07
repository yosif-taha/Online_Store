using AutoMapper;
using Online_Store.Domain.Entites.Identity;
using Online_Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
