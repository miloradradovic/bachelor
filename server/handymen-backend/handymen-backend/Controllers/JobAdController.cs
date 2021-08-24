using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/job-ads")]
    [EnableCors("CorsPolicy")]
    public class JobAdController : ControllerBase
    {

        private readonly IJobAdService _jobAdService;

        public JobAdController(IJobAdService jobAdService) 
        {
            _jobAdService = jobAdService;
        }

        [HttpPost("create-job-ad")]
        [Authorize(Roles.USER)]
        public IActionResult CreateJobAd([FromBody] JobAdDTO jobAdDto)
        {
            ApiResponse response = _jobAdService.CreateJobAd(jobAdDto.ToJobAd(), jobAdDto.Trades, (User) HttpContext.Items["LoggedIn"]);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

        [HttpGet("get-job-ads-by-handyman")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult GetJobAdsForCurrentHandyman()
        {
            ApiResponse response = _jobAdService.GetJobAdsForCurrentHandyman((HandyMan) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }

        [HttpGet("get-job-ads-by-user")]
        [Authorize(Roles.USER)]
        public IActionResult GetJobAdsByUser()
        {
            ApiResponse response = _jobAdService.GetJobAdsByUser((User) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }

        [HttpGet("get-job-ads-by-user-no-offer")]
        [Authorize(Roles.USER)]
        public IActionResult GetJobAdsByUserWithNoOffer()
        {
            ApiResponse response = _jobAdService.GetJobAdsByUserNoOffer((User) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}