using Authentication_Service.Model;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication_Service.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _config;
        public AuthRepo(UserDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public AuthenticationResult AuthenticateUser(string username, string password)
        {
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
               
                if (user.Password == password)
                {
                    var tokenValue = GetJWTToken(username);

                    var jwtToken = new JwtToken
                    {
                        TokenValue = tokenValue,
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        LastLogin = DateTime.UtcNow,
                        UserId = user.UserId
                    };

                    //_dbContext.JwtTokens.Add(jwtToken);
                    //_dbContext.SaveChanges();

                    
                    return new AuthenticationResult
                    {
                        message = "Login successful",
                        UserId = user.UserId,
                        Username = username,
                        TokenValue = tokenValue,
                        ExpirationDate = jwtToken.ExpirationDate.Value
                    };
                }
                else { return null; }
            }

            return null; 
        }

        private string GetJWTToken(string username)
        {
            
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
            };

           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_auth_jwt_to_secure_microservice"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            
            var token = new JwtSecurityToken(
                issuer: "AuthenticationServer",
                audience: "AuthClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
