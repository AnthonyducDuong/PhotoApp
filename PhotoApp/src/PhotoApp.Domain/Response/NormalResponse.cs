using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Response
{
    public class NormalResponse
    {
        public bool Success { get; set; }
        
        public string? Message { get; set; }

        public IEnumerable<string>? Errors { get; set; }
        
        public NormalResponse() { }

        public NormalResponse(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
