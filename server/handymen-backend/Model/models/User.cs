﻿using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class User : Person
    {
        public virtual List<JobAd> JobAds { get; set; }
        
        public virtual List<Job> Jobs { get; set; }
        
        public Location Address { get; set; }

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
        
        public ProfileDataDTO ToProfileDataDTO()
        {
            return new ProfileDataDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Location = new LocationDTO()
                {
                    Id = Address.Id,
                    Latitude = Address.Latitude,
                    Longitude = Address.Longitude,
                    Name = Address.Name
                }
            };
        }
        
    }
}