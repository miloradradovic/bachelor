using System.Collections.Generic;

namespace Model.models
{
    public class HandyMan : Person
    {
        public virtual List<Trade> Trades { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Job> DoneJobs { get; set; }
    }
}