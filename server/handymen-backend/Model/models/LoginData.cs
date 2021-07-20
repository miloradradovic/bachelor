using Model.dto;

namespace Model.models
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginDataDTO ToDto()
        {
            return new LoginDataDTO()
            {
                Password = Password,
                Username = Username
            };
        }
    }
}