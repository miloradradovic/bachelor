using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class Trade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<HandyMan> HandyMen { get; set; }
        
        public virtual List<JobAd> JobAds { get; set; }

        public TradeDTO ToTradeDTO()
        {
            return new TradeDTO()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}