using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class HandyMan : Person
    {
        public virtual List<Trade> Trades { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Job> DoneJobs { get; set; }
        public Location Circle { get; set; }

        public HandymanDTO ToDtoWithTrades()
        {
            return new HandymanDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Password = Password,
                Role = Role.ToString(),
                Trades = TradesToString(),
                Verified = Verified
            };
        }

        public HandymanDTO ToDtoWithoutLists()
        {
            return new HandymanDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Password = Password,
                Role = Role.ToString(),
                Verified = Verified
            };
        }

        private List<string> TradesToString()
        {
            List<string> trades = new List<string>();
            
            foreach (Trade trade in Trades)
            {
                trades.Add(trade.Name);
            }

            return trades;
        }
    }
}