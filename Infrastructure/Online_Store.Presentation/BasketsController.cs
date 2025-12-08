using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store.Services;
using Online_Store.Shared.Dtos.Baskets;

namespace Online_Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServicesManager _servicesManager) : ControllerBase
    {
        [HttpGet] //Get : badeUrl/api/Baskets?id
        public async Task<IActionResult> GetBasketById(string id)
        {
          var result = await _servicesManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }
       [HttpPost]//Post : badeUrl/api/Baskets
        public async Task<IActionResult> CreateOrUpdateBasket(CustomerBasketDto  dto)
        {
            var result = await _servicesManager.BasketService.CreateBasketAsync(dto ,TimeSpan.FromDays(1));
            return Ok(result);
        }
        [HttpDelete]//Delete : badeUrl/api/Basket
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await _servicesManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
