using PhotoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class CommentRequest
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Comment is invalid")]
        [DataType(DataType.Text)]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Status is invalid")]
        public StatusCommentEnums Status { get; set; }

        [Required(ErrorMessage = "Date is invalid")]
        public DateTime Date { get; set; }
        
        public string? UserId { get; set; }

        public string? PhotoId { get; set; }

        public string? CommentId { get; set; }
    }
}
