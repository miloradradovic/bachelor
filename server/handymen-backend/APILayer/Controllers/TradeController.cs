using System.Collections.Generic;
using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/trades")]
    [EnableCors("CorsPolicy")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("get-trades")]
        public IActionResult GetAllTrades()
        {
            ApiResponse response = _tradeService.GetAll();

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-trades/{professionId}")]
        public IActionResult GetTradesByProfession([FromRoute] int professionId)
        {
            ApiResponse response = _tradeService.GetTradesByProfession(professionId);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-trades-by-profname/{professionName}")]
        public IActionResult GetTradesByProfessionName([FromRoute] string professionName)
        {
            ApiResponse response = _tradeService.GetTradesByProfessionName(professionName);
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-profession-and-category-by-trades")]
        [Authorize(Roles.HANDYMAN)]
        public IActionResult GetProfessionAndCategoryByCurrentHandymanTrades()
        {
            ApiResponse response = _tradeService.GetCategoryAndProfessionByCurrentHandyman((HandyMan) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}