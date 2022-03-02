using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GenerateAccessToken(UserEntity userEntity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // hash key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            // payload
#pragma warning disable CS8604 // Possible null reference argument.
            var claims = new[]
            {
                new Claim("First Name", userEntity.FirstName),
                new Claim("Last Name", userEntity.LastName),
                new Claim(ClaimTypes.Name, userEntity.UserName),
                new Claim(ClaimTypes.Email, userEntity.Email),
            };
#pragma warning restore CS8604 // Possible null reference argument.

            // create token expired after 15 minutes
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(JwtExpireConstants.EXPIRE_ACCESS_TOKEN),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // hash key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            // payload
#pragma warning disable CS8604 // Possible null reference argument.
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
            };
#pragma warning restore CS8604 // Possible null reference argument.

            // create token expired after 7 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(JwtExpireConstants.EXPIRE_REFRESH_TOKEN),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
