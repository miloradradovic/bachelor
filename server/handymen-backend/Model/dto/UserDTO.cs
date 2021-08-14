using System;
using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        
        public LocationDTO Location { get; set; }
        
        
        public User ToEntity()
        {
            return new User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Role = Enum.Parse<Role>(Role),
                Email = Email,
                Password = Password,
                Verified = Verified,
                Address = Location.ToLocation()
            };
        }
        
    }
}