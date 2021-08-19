using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
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
        //private readonly UserManager<Person> _userManager;

        public JobAdController(IJobAdService jobAdService) 
        {
            _jobAdService = jobAdService;
            //_userManager = userManager;
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

        [HttpGet("get-job-ads-for-current")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult GetJobAdsForCurrentHandyman()
        {
            ApiResponse response = _jobAdService.GetJobAdsForCurrentHandyman((HandyMan) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}