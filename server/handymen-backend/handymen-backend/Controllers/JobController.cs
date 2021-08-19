using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    [EnableCors("CorsPolicy")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("create-job/{interest}")]
        [Authorize(Roles.USER)]
        public IActionResult CreateJob([FromRoute] int interest)
        {
            ApiResponse response = _jobService.CreateJob(interest);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

        [HttpPut("finish-job/{jobId}")]
        [Authorize(Roles.HANDYMAN, Roles.USER)]
        public IActionResult FinishJob([FromRoute] int jobId)
        {
            ApiResponse response = _jobService.FinishJob(jobId);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}