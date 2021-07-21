using System.Collections.Generic;

namespace Model.models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Trade> Trades { get; set; }
    }
}