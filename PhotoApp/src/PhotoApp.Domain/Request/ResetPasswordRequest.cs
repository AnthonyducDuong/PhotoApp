using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Token invalid")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Email invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "New Password in valid")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password in valid")]
        public string? ConfirmPassword { get; set; }
    }
}
