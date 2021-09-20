using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/offers")]
    [EnableCors("CorsPolicy")]
    public class OfferController: ControllerBase
    {

        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        
        [HttpPost("make-offer")]
        [Authorize(Roles.USER)]
        public IActionResult MakeOffer([FromBody] OfferDTO offerDto)
        {
            ApiResponse response = _offerService.MakeOffer(offerDto.HandyMan, offerDto.JobAd);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("get-offers-by-handyman")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult GetOffersByHandyman()
        {
            ApiResponse response = _offerService.GetOffersByHandyman((HandyMan) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}