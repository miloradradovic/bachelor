using System;
using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class RegistrationRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public List<string> Trades { get; set; }
        
        public User ToUser()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Role.USER,
                Verified = false
            };
        }

        public HandyMan ToHandyman()
        {
            return new HandyMan()
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Role = Role.HANDYMAN,
                Verified = false
                // list of trades will be mapped later
            };
        }
    }
}