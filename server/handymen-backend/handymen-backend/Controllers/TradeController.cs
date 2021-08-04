using System.Collections.Generic;
using BusinessLogicLayer.services;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("trades")]
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
            List<Trade> trades = _tradeService.GetAll();
            return Ok(trades);
        }
    }
}