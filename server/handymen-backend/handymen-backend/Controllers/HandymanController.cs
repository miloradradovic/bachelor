using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/handymen")]
    [EnableCors("CorsPolicy")]
    public class HandymanController : ControllerBase
    {

        private readonly IPersonService _personService;
        
        public HandymanController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("register")]
        public IActionResult CreateRegistrationRequest([FromBody] RegistrationRequestDTO requestDto)
        {
            ApiResponse response = _personService.RegisterHandyman(requestDto.ToHandyman(), requestDto.Trades);

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

        [HttpPut("verify")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult VerifyRegistrationRequest(
            [FromBody] HandymanVerificationDataDTO handymanVerificationDataDto)
        {
            ApiResponse response =
                _personService.VerifyHandyman(handymanVerificationDataDto.ToHandymanVerificationData());

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}