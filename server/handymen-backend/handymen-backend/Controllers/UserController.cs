using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BusinessLogicLayer.services;
using handymen_backend.jwt;


namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: ControllerBase
    {
        private readonly IPersonService _personService;
        
        public UserController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("register")]
        public IActionResult CreateRegistrationRequest([FromBody] RegistrationRequestDTO userRegistrationRequestDto)
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
            
            if (response.Status == 404)
            {
                return NotFound(response);
            }

            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
