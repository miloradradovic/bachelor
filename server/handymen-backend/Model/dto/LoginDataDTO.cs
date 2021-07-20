using Model.models;

namespace Model.dto
{
    public class LoginDataDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Token { get; set; }

        public LoginData ToEntity()
        {
            return new LoginData()
            {
                Password = Password,
                Username = Username
            };
        }
    }
}