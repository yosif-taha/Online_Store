using Online_Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions.Auth
{
    public interface IAuthService
    {
       Task<UserResponse?> LoginAsync(LoginRequest request);
       Task<UserResponse?> RegiserAsync(RegisterRequest request);

        //Check Email Exists
        Task<bool> ChekEmailExistsAsync(string email);

        //Get Current User
        Task<UserResponse?> GetCurrentUserAsync(string email);

        //Get Current User Address
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);

        //Update Current User Adderess
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request,string email);


    }
}
