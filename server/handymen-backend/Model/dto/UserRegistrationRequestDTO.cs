using System;
using Model.models;

namespace Model.dto
{
    public class UserRegistrationRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public RegistrationRequest ToRegistrationRequest()
        {
            return new RegistrationRequest()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Role.USER
            };
        }
    }
}