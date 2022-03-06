using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class EmailUserRequest
    {
        [Required(ErrorMessage = "email invalid")]
        public string Email { get; set; }
    }
}
