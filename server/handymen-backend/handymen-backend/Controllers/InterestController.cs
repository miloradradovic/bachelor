using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/interests")]
    public class InterestController : ControllerBase
    {

        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpPost]
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
    }
}