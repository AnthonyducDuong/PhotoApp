using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class CommentUpdateRequest
    {
        public string? Id { get; init; }

        public string? Content { get; set; }
    }
}
