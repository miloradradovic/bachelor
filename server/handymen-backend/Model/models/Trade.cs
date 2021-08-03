using System.Collections.Generic;

namespace Model.models
{
    public class Trade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<HandyMan> HandyMen { get; set; }
    }
}