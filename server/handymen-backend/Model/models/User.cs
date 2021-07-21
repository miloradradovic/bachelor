using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class User : Person
    {
        public virtual List<JobAd> JobAds { get; set; }
        
        public virtual List<Job> Jobs { get; set; }
        
    }
}