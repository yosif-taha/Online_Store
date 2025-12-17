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
      public Task<UserResponse?> LoginAsync(LoginRequest request);
      public Task<UserResponse?> RegiserAsync(RegisterRequest request);
    }
}
