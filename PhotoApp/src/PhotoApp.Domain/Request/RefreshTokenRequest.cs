using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Refresh token invalid")]
        public string? refreshToken { get; set; }
    }
}
