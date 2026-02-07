using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using Online_Store.Shared.Dtos.Orders;
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
    public class OrdersController(IServicesManager _servicesManager) : ControllerBase
    {
        [HttpPost] //Post baseUrl/api/Order
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.OrderServices.CreateOrderAsync(orderRequest, userEmail.Value);
          return Ok(result);
        }

        [HttpGet("{id}")] //Get BaseUrl/api/Order/{id}
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);

            var result =  await _servicesManager.OrderServices.GetOrderForSpecificUserAsync(id, userEmail.Value);
            return Ok(result);
        }
        [HttpGet] //Get BaseUrl/api/Order/Orders
        public async Task<IActionResult> GetOrders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);

            var result = await _servicesManager.OrderServices.GetOrdersForSpecificUserAsync(userEmail.Value);
            return Ok(result);
        }
        [HttpGet("DeliveryMethods")] //Get BaseUrl/api/Order/DeliveryMethods
        public async Task<IActionResult> GetDeliveryMethod()
        {
          var result =  await _servicesManager.OrderServices.GetDeliveryMethodsAsync();
            return Ok(result);
        }

    }
}
