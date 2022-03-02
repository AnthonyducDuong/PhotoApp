using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "UserName is invalid")]
        [StringLength(50, ErrorMessage = "Your Username can contain only 50 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is invalid")]
        [StringLength(50, ErrorMessage = "Your Password can contain only 50 characters")]
        public string? Password { get; set; }
    }
}
