using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public string ValidateJwtToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            try
            {
                tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    // ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string email = jwtToken.Claims.First(c => c.Type == "email").Value;

                // Check token expired
                /*string exp = jwtToken.Claims.First(c => c.Type == "exp").Value;
                long exp_long = long.Parse(exp);

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(exp_long);
                DateTime dateTimeExpires = dateTimeOffset.LocalDateTime;

                if (dateTimeExpires < DateTime.UtcNow)
                {
                    return "";
                }*/

                // return email from JWT token if validation successful
                return email;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
