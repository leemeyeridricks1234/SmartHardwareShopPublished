using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartHardwareShop.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbContextFactory<SmartHardwareShopContext> contextFactory;
        private readonly IConfiguration configuration;

        public AuthRepository(IDbContextFactory<SmartHardwareShopContext> contextFactory, IConfiguration configuration)
        {
            this.contextFactory = contextFactory;
            this.configuration = configuration;
        }
        public string Login(string username, string password)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var user = context.User.FirstOrDefault(user =>
                                user.Username.ToLower() == username.ToLower()
                                          && user.Password.ToLower() == password.ToLower());
                if (user == null)
                {
                    throw new ApplicationException("User not found: " + username);
                }
                return CreateToken(user); 
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
