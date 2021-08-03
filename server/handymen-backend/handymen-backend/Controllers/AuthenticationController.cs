using Microsoft.AspNetCore.Mvc;
using Model.dto;
using BusinessLogicLayer.services;
using handymen_backend.jwt;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController: ControllerBase
    {
        private IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("log-in")]
        public IActionResult LogIn(LoginDataDTO loginDataDto)
        {
            var response = _authService.LogIn(loginDataDto.ToEntity());

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(new TokenDataDTO()
            {
                LoginDataDto = loginDataDto,
                Token = response
            });
        }
        
        /*
        [Authorize(Roles.ADMINISTRATOR)]
        [HttpPost("test-admin")]
        public IActionResult TestAdmin()
        {
            return Ok();
        }
        
        [Authorize(Roles.HANDYMAN)]
        [HttpPost("test-handyman")]
        public IActionResult TestHandyman()
        {
            return Ok();
        }
        
        [Authorize(Roles.USER)]
        [HttpPost("test-user")]
        public IActionResult TestUser()
        {
            return Ok();
        }
        */
    }
}