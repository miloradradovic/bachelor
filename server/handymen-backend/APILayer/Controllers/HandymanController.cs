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
        public IActionResult CreateRegistrationRequest([FromBody] RegistrationRequestDataDTO requestDto)
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

        [HttpGet]
        [Authorize(Roles.USER, Roles.ADMINISTRATOR)]
        public IActionResult GetAllHandymen()
        {
            ApiResponse response = _personService.GetAllHandymen();
            return Ok(response);
        }
        
        [HttpPost("search")]
        [Authorize(Roles.USER)]
        public IActionResult Search([FromBody] SearchFilterParams searchParams)
        {
            ApiResponse response = _personService.SearchHandymen(searchParams);

            return Ok(response);
        }
        
        [HttpPost("filter")]
        [Authorize(Roles.USER)]
        public IActionResult Filter([FromBody] SearchFilterParams searchParams)
        {
            ApiResponse response = _personService.FilterHandymen(searchParams);

            return Ok(response);
        }

        [HttpGet("get-handyman-by-id/{handymanId}")]
        [Authorize(Roles.USER, Roles.HANDYMAN)]
        public IActionResult GetHandymanById([FromRoute] int handymanId)
        {
            ApiResponse response = _personService.GetHandymanByIdApiResponse(handymanId);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-current-handyman")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult GetCurrentHandyman()
        {
            HandyMan current = (HandyMan) HttpContext.Items["LoggedIn"];
            ApiResponse response = new ApiResponse();
            response.UpdatedHandymanProfile(current, "Uspesno dobavljen trenutno ulogovani majstor.", 200);
            return Ok(response);
        }

        [HttpPut("edit-profile")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult EditProfile([FromBody] ProfileDataDTO profileDataDto)
        {
            ApiResponse response =
                _personService.EditProfile(profileDataDto.ToHandyman(), profileDataDto.Trades, "handyman");

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-requests")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult GetRequests()
        {
            ApiResponse response = _personService.GetUnverifiedHandymen();
            return Ok(response);
        }

        [HttpGet("get-by-profession/{professionName}")]
        [Authorize(Roles.USER)]
        public IActionResult GetByProfession([FromRoute] string professionName)
        {
            ApiResponse response = _personService.GetHandymenByProfessionName(professionName);
            return Ok(response);
        }
    }
}