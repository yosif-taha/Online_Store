using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Presentation
{

    [Controller]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase 
    {

        [HttpGet("notfound")]
        public IActionResult GetNotFoundResponse() //Get : baseUrl/api/buggy/notfound
        {
            //logic

            return NotFound();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequestResponse() //Get :baseUrl/api/buggy/badrequest
        {

            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationErrorResponse(int id) //Get :baseUrl/api/buggy/badrequest/2
        {

            return BadRequest();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErrorResponse() //Get :baseUrl/api/buggy/servererror
        {
            throw new Exception("Eroor");
            return BadRequest();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedErrorResponse() //Get :baseUrl/api/buggy/unauthorized
        {

            return Unauthorized();
        }

    }
}
