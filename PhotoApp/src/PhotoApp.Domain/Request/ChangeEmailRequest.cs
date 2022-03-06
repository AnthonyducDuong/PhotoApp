using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class ChangeEmailRequest
    {
        [Required(ErrorMessage = "Current Email invalid")]
        public string? CurrentEmail { get; set; }

        [Required(ErrorMessage = "New Email invalid")]
        public string? NewEmail { get; set; }
    }
}
