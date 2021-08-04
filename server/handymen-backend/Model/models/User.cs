using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class User : Person
    {
        public virtual List<JobAd> JobAds { get; set; }
        
        public virtual List<Job> Jobs { get; set; }

        public UserDTO ToDtoWithoutLists()
        {
            return new UserDTO()
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
        
    }
}