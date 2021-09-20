using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/ratings")]
    [EnableCors("CorsPolicy")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("create-rating")]
        [Authorize(Roles.USER)]
        public IActionResult CreateRating([FromBody] RatingDTO ratingDto)
        {
            ApiResponse response = _ratingService.CreateRating(ratingDto.ToRating(), ratingDto.JobId);

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

        [HttpGet("get-unverified-ratings")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult GetUnverifiedRatings()
        {
            ApiResponse response = _ratingService.GetRatings(false);
            return Ok(response);
        }

        [HttpPut("verify-rating/{ratingId}")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult VerifyRating([FromRoute] int ratingId)
        {
            ApiResponse response = _ratingService.VerifyRating(ratingId);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete-rating/{ratingId}")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult DeleteRating([FromRoute] int ratingId)
        {
            ApiResponse response = _ratingService.DeleteRating(ratingId);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}