using System;
using Model.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.dto;

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
            Person toLogIn = _personService.GetByEmailAndPassword(loginData.Email, loginData.Password);

            if (toLogIn == null)
            {
                return new ApiResponse()
                {
                    Message = "Kombinacija emaila i lozinke nije ispravna.",
                    ResponseObject = null,
                    Status = 401
                };
            }

            string token = GenerateJwtToken(toLogIn);

            return new ApiResponse()
            {
                Message = "Uspesno ste se ulogovali.",
                ResponseObject = token,
                Status = 200
            };
        }

        private string GenerateJwtToken(Person toLogIn)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", toLogIn.Id.ToString()), new Claim("email", toLogIn.Email), new Claim("role", toLogIn.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}