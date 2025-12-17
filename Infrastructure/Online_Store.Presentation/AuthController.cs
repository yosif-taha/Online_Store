using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using Online_Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
