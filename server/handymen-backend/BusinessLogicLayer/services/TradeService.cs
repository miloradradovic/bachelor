using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ITradeService
    {
        public Trade GetByName(string name);
        public Trade Create(Trade trade);
        public List<Trade> GetAll();
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

        public List<Trade> GetAll()
        {
            return _tradeRepository.GetAll();
        }

        public void UpdateTrades(HandyMan updatedHandyman)
        {
            foreach (Trade trade in updatedHandyman.Trades)
            {
                try
                {
                    trade.HandyMen.Add(updatedHandyman);
                }
                catch (Exception e)
                {
                    trade.HandyMen = new List<HandyMan>();
                    trade.HandyMen.Add(updatedHandyman);
                }

                _tradeRepository.Update(trade);
            }
        }
    }
}