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
        public ApiResponse GetTradesByProfession(int professionId);
    }
    
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _tradeRepository;
        private readonly IProfessionService _professionService;

        public TradeService(ITradeRepository tradeRepository, IProfessionService professionService)
        {
            _tradeRepository = tradeRepository;
            _professionService = professionService;
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

        public ApiResponse GetTradesByProfession(int professionId)
        {
            Profession profession = _professionService.GetById(professionId);
            if (profession == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find profession by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<TradeDTO> dtos = new List<TradeDTO>();
            foreach (Trade trade in profession.Trades)
            {
                dtos.Add(trade.ToTradeDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched trades by profession.",
                ResponseObject = dtos,
                Status = 200
            };
        }
    }
}