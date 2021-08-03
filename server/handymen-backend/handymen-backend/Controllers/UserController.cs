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

        [HttpPost]
        public IActionResult CreateRegistrationRequest(UserRegistrationRequestDTO userRegistrationRequestDto)
        {
            ApiResponse response = _UserService.CreateRegistrationRequest(userRegistrationRequestDto.ToRegistrationRequest());

            if (response.Status == 200)
            {
                return Ok(response);
            } else if (response.Status == 201)
            {
                return Created(response.Message, response);
            }
            
            return BadRequest(response);
        }

        /*
        [HttpGet]
        public IActionResult GetUserBySomethings()
        {
            Something something = new Something()
            {
                Id = 1,
                name = "something1"
            };
            List<User> users = _UserService.GetUsersBySomethings(something);
            return Ok(users);
        }
        */
    }
}
