using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/interests")]
    [EnableCors("CorsPolicy")]
    public class InterestController : ControllerBase
    {

        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpPost("create-interest")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult MakeInterest([FromBody] InterestDTO interestDto)
        {
            ApiResponse response = _interestService.MakeInterest(interestDto.ToInterest(), interestDto.JobAdId, (HandyMan) HttpContext.Items["LoggedIn"]);

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

        [HttpGet("get-interests-by-user")]
        [Authorize(Roles.USER)]
        public IActionResult GetInterestsByUser()
        {
            ApiResponse response = _interestService.GetByUser((User) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}