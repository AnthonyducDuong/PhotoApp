using PhotoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IServices
{
    public interface IJwtService
    {
        public string GenerateAccessToken(UserEntity userEntity);
        public string GenerateRefreshToken(string email);

        public string ValidateJwtToken(string refreshToken);
    }
}
