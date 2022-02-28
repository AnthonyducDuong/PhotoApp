using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Models
{
    public class UserModel
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Username is invalid")]
        [StringLength(50, ErrorMessage = "Your Username can contain only 20 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "First name is invalid")]
        [StringLength(50, ErrorMessage = "Your First Name can contain only 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is invalid")]
        [StringLength(20, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        [Required(ErrorMessage = "Email is invalid")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is invalid")]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is invalid")]
        [DataType(DataType.Password, ErrorMessage = "ConfirmPassword is invalid")]
        public string? ConfirmPassword { get; set; }
    }
}
