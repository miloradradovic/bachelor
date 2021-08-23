using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class ProfileDataDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public LocationDTO Location { get; set; }
        public List<string> Trades { get; set; }

        public HandyMan ToHandyman()
        {
            return new HandyMan()
            {
                Address = Location.ToLocation(),
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Radius = Location.Radius
            };
        }

        public User ToUser()
        {
            return new User()
            {
                Address = Location.ToLocation(),
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName
            };
        }
    }
}