using System;
using Model.models;

namespace Model.dto
{
    public class AdministratorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
        
        public bool Verified { get; set; }

        public Administrator ToAdministratorWithoutId()
        {
            return new Administrator()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Enum.Parse<Role>(Role),
                Verified = Verified
            };
        }
        
        public Administrator ToAdministratorWithId()
        {
            return new Administrator()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Enum.Parse<Role>(Role),
                Verified = Verified
            };
        }
    }
}