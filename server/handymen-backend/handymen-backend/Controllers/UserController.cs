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
        public readonly IUserService _UserService;
        
        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpPost("register")]
        public IActionResult CreateRegistrationRequest([FromBody] UserRegistrationRequestDTO userRegistrationRequestDto)
        {
            ApiResponse response = _UserService.RegisterUser(userRegistrationRequestDto.ToRegistrationRequest());
            
            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            
            return Created(response.Message, response);
        }

        [HttpGet("verify/{encrypted}")]
        public IActionResult VerifyRegistrationRequest([FromRoute] string encrypted)
        {
            ApiResponse response = _UserService.VerifyUser(encrypted);
            
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
