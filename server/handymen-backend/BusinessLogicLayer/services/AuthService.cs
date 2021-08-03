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
        private readonly IUserService _userService;
        private readonly IAdministratorService _administratorService;
        private readonly IHandymanService _handymanService;

        public AuthService(IUserService userService, IAdministratorService administratorService,
            IHandymanService handymanService, IConfiguration configuration)
        {
            _userService = userService;
            _administratorService = administratorService;
            _handymanService = handymanService;
            _configuration = configuration;
        }
        
        public ApiResponse LogIn(LoginData loginData)
        {
            Administrator administrator = _administratorService.GetByEmailAndPassword(loginData.Email, loginData.Password);
            User user = _userService.GetByEmailAndPassword(loginData.Email, loginData.Password);
            HandyMan handyMan = _handymanService.GetByEmailAndPassword(loginData.Email, loginData.Password);
            Person toLogIn = null;
            
            if (user == null && administrator == null && handyMan == null)
            {
                return new ApiResponse()
                {
                    Message = "Bad credentials.",
                    ResponseObject = null,
                    Status = 401
                };
            }

            if (user != null)
            {
                toLogIn = user;
            } 
            else if (administrator != null)
            {
                toLogIn = administrator;
            }
            else
            {
                toLogIn = handyMan;
            }

            string token = GenerateJwtToken(toLogIn);

            return new ApiResponse()
            {
                Message = "Successfully logged in.",
                ResponseObject = new TokenDataDTO()
                {
                    LoginDataDto = loginData.ToDto(),
                    Token = token
                },
                Status = 200
            };
        }

        private string GenerateJwtToken(Person toLogIn)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", toLogIn.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}