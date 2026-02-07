using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using Online_Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServicesManager _servicesManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
          var result = await  _servicesManager.AuthServices.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
          var result = await  _servicesManager.AuthServices.RegiserAsync(request);
            return Ok(result);
        }

        //Check Email Exists
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery]string email)
        {
          var result = await  _servicesManager.AuthServices.ChekEmailExistsAsync(email);
            return Ok(result);
        }

        //Get Current User
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.AuthServices.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        //Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.AuthServices.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }

        //Update Current User Adderess
        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.AuthServices.UpdateCurrentUserAddressAsync(address,email.Value);
            return Ok(result);
        }
    }
}
