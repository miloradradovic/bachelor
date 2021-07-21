using Model.models;

namespace Model.dto
{
    public class LoginDataDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginData ToEntity()
        {
            return new ()
            {
                Password = Password,
                Email = Email
            };
        }
    }
}