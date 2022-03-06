using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class BasicInfoUserRequest
    {
        [Required(ErrorMessage = "Email invalid")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "Your Username can contain only 50 characters")]
        public string? UserName { get; set; }

        [StringLength(50, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Your Last Name can contain only 50 characters")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        public string? Password { get; set; }
    }
}
