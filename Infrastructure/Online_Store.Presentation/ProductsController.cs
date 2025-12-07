using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using Online_Store.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServicesManager _servicesManager) : ControllerBase
    {

        [HttpGet] //Get: baseUrl/api/Products
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters parameters)
        {
            var result = await _servicesManager.ProductService.GetAllProductAsync(parameters);
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }


        [HttpGet("{id}")]  //Get: baseUrl/api/Products/5
        public async Task<IActionResult> GetProductsById(int? id)
        {
            var result = await _servicesManager.ProductService.GetProductByIdAsync(id.Value);
            return Ok(result); //200
        }

        [HttpGet("brands")] //Get: baseUrl/api/Products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _servicesManager.ProductService.GetAllBrandtAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }

        [HttpGet("types")] //Get: baseUrl/api/Products/brands/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _servicesManager.ProductService.GetAllTypeAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }


    }
}
