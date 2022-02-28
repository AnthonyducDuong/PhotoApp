using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Wrappers
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public Response() { }

        public Response(T data, string? message = null) 
        {
            this.Success = true;
            this.Message = message;
            this.Data = data;
        }

        public Response(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
