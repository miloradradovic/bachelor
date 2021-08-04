using System.Collections.Generic;

namespace Model.dto
{
    public class HandymanDTO
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
        
        public bool Verified { get; set; }
        
        public List<string> Trades { get; set; }
    }
}