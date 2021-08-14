using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ITradeService
    {
        public Trade GetByName(string name);
        public Trade Create(Trade trade);
        public ApiResponse GetAll();
        public void UpdateTrades(HandyMan updatedHandyman);
    }
    
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public Trade GetByName(string name)
        {
            return _tradeRepository.GetByName(name);
        }

        public Trade Create(Trade trade)
        {
            if (_tradeRepository.GetByName(trade.Name) == null)
            {
                return _tradeRepository.Create(trade);
            }

            return null;
        }

        public ApiResponse GetAll()
        {
            List<Trade> trades = _tradeRepository.GetAll();
            if (trades == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database while fetching trades. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<TradeDTO> tradeDtos = new List<TradeDTO>();
            foreach (Trade trade in trades)
            {
                tradeDtos.Add(trade.ToTradeDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched all trades.",
                ResponseObject = tradeDtos,
                Status = 200
            };
        }

        public void UpdateTrades(HandyMan updatedHandyman)
        {
            foreach (Trade trade in updatedHandyman.Trades)
            {
                try
                {
                    trade.HandyMen.Add(updatedHandyman);
                }
                catch
                {
                    trade.HandyMen = new List<HandyMan>();
                    trade.HandyMen.Add(updatedHandyman);
                }

                _tradeRepository.Update(trade);
            }
        }
    }
}