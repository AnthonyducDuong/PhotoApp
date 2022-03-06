using PhotoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Request
{
    public class PhotoRequest
    {
        public string? Description { get; set; }

        [Required(ErrorMessage = "Photo URL is invalid")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Mode is invalid")]
        public ModePhotoEnums Mode { get; set; }

        [Required(ErrorMessage = "CreatedAt is invalid")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated is invalid")]
        public DateTime Updated { get; set; }

        // Foreign Key
        [Required]
        public Guid UserId { get; set; }
    }
}
