using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.models;

namespace handymen_backend.jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IAdministratorService administratorService, IHandymanService handymanService,
            IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, administratorService, handymanService, userService, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IAdministratorService administratorService, 
            IHandymanService handymanService, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var personId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                Person loggedIn = findLoggedIn(administratorService, handymanService, userService, personId);

                context.Items["LoggedIn"] = loggedIn;
            }
            catch
            {

            }
        }

        private Person findLoggedIn(IAdministratorService administratorService,
            IHandymanService handymanService, IUserService userService, int personId)
        {
            User foundUser = userService.GetById(personId);
            if (foundUser != null)
            {
                return foundUser;
            }

            HandyMan foundHandyMan = handymanService.GetById(personId);
            if (foundHandyMan != null)
            {
                return foundHandyMan;
            }

            Administrator foundAdministrator = administratorService.GetById(personId);
            if (foundAdministrator != null)
            {
                return foundAdministrator;
            }

            return null;
        }
    }
}