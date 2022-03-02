using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Response
{
    public class RegisterResponse
    {
        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public bool IsVerified { get; set; }
    }
}
