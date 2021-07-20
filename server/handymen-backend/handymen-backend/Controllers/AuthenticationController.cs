using Microsoft.AspNetCore.Mvc;
using Model.dto;
using BusinessLogicLayer.services;

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

            return Ok(new LoginDataDTO()
            {
                Password = loginDataDto.Password,
                Username = loginDataDto.Username,
                Token = response
            });
        }
    }
}