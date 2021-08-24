using System;
using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class RegistrationRequestDataDTO
    {
        public int Id { get; set; } // handyman id
        public double Radius { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public LocationDTO Location { get; set; }
        
        public List<string> Trades { get; set; }

        public HandyMan ToHandyman()
        {
            return new HandyMan()
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Role = Role.HANDYMAN,
                Verified = false,
                Radius = Location.Radius,
                Address = Location.ToLocation()
                // list of trades will be mapped later
            };
        }
        
        public User ToUser()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Role.USER,
                Verified = false,
                Address = Location.ToLocation()
            };
        }
    }
}