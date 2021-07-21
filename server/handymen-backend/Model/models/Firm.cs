using System.Collections.Generic;

namespace Model.models
{
    public class Firm
    {
        public int Id { get; set; }
        public int PIB { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public Location Address { get; set; }
        public virtual List<Trade> Trades { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        
    }
}