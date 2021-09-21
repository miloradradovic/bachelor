using System;
using Model.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.services
{
    
    public interface IAuthService
    {
        public ApiResponse LogIn(LoginData loginData);

    }
    
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly IPersonService _personService;

        public AuthService(IPersonService personService, IConfiguration configuration)
        {
            _personService = personService;
            _configuration = configuration;
        }
        
        public ApiResponse LogIn(LoginData loginData)
        {
            ApiResponse response = new ApiResponse();
            Person toLogIn = _personService.GetByEmailAndPassword(loginData.Email, loginData.Password);

            if (toLogIn == null)
            {
                response.SetError("Kombinacija emaila i lozinke nije ispravna.", 401);
                return response;
            }

            string token = GenerateJwtToken(toLogIn);
            response.LoggedIn(token, "Uspesno ste se ulogovali.", 200);
            return response;
        }

        private string GenerateJwtToken(Person toLogIn)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", toLogIn.Id.ToString()), new Claim("email", toLogIn.Email), 
                    new Claim("role", toLogIn.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}