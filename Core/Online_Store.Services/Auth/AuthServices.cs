using Microsoft.AspNetCore.Identity;
using Online_Store.Domain.Entites.Identity;
using Online_Store.Domain.Exeptions.BadRequest;
using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Domain.Exeptions.UnAuthorized;
using Online_Store.Services.Abstractions.Auth;
using Online_Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Online_Store.Shared;

namespace Online_Store.Services.Auth
{
    public class AuthServices(UserManager<AppUser> _userManager, IOptions<JwtOptions> _options) : IAuthService
    {
        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new UserNotFoundException(request.Email);
            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnAuthorizedException();

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };

        }

        public async Task<UserResponse?> RegiserAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,
            };


            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegesterBadRequestException(result.Errors.Select(e => e.Description).ToList());

             return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };

        }

        private async Task<string> GenerateTokenAsync(AppUser user)
        {

            var authClaims = new List<Claim>()
            {
              new Claim(ClaimTypes.GivenName,user.DisplayName) ,
              new Claim(ClaimTypes.Email,user.Email) ,
              new Claim(ClaimTypes.MobilePhone,user.PhoneNumber) ,

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var option = _options.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.key));

            var token = new JwtSecurityToken(
                issuer: option.issuer,
                audience:option.auduance,
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(option.durationindays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
        
           return new JwtSecurityTokenHandler().WriteToken(token);
        
        }
    }
}
