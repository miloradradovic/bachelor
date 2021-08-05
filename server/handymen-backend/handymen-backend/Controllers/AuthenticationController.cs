using Microsoft.AspNetCore.Mvc;
using Model.dto;
using BusinessLogicLayer.services;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
            ApiResponse response = _authService.LogIn(loginDataDto.ToEntity());

            if (response.Status == 401)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}