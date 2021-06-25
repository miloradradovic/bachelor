using Contracts.services;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


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
        public IActionResult CreateUser(UserDTO userDto)
        {
            User saved = _UserService.CreateUser(userDto.toEntity());
            return Ok(saved.toDto());
        }
    }
}
