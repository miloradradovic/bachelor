using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;
using BusinessLogicLayer.services;
using Microsoft.AspNetCore.Cors;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    [EnableCors("CorsPolicy")]
    public class UserController: ControllerBase
    {
        private readonly IPersonService _personService;
        
        public UserController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("register")]
        public IActionResult CreateRegistrationRequest([FromBody] RegistrationRequestDataDTO userRegistrationRequestDto)
        {
            ApiResponse response = _personService.RegisterUser(userRegistrationRequestDto.ToUser());
            
            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            
            return Created(response.Message, response);
        }

        [HttpGet("verify/{encrypted}")]
        public IActionResult VerifyRegistrationRequest([FromRoute] string encrypted)
        {
            ApiResponse response = _personService.VerifyUser(encrypted);

            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
