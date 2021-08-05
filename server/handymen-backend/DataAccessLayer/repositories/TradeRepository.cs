using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface ITradeRepository
    {
        public Trade GetByName(string name);
        public Trade Create(Trade trade);
        public List<Trade> GetAll();
        public Trade Update(Trade toUpdate);
    }
    
    public class TradeRepository : ITradeRepository
    {
        private readonly PostgreSqlContext _context;

        public TradeRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Trade GetByName(string name)
        {
            Trade found = _context.Trades.SingleOrDefault(trade => trade.Name == name);
            return found;
        }

        public Trade Create(Trade trade)
        {
            _context.Trades.Add(trade);
            _context.SaveChanges();
            return trade;
        }

        public List<Trade> GetAll()
        {
            //return _context.Trades.ToList();
            IEnumerable<Trade> trades = 
                from trade in _context.Trades.Include(trade => trade.HandyMen)
                select trade;
            
            return trades.ToList();
        }

        public Trade Update(Trade toUpdate)
        {
            _context.Trades.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }
    }
}