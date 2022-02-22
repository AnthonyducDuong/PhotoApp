using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Entities
{
    public class LikePhotoEntity
    {
        // Foreign Key
        // User Entity
        public Guid UserId { get; set; }
        public UserEntity? UserEntity { get; set; }

        // Photo Entity
        public Guid PhotoId { get; set; }
        public PhotoEntity? PhotoEntity { get; set; }
    }
}
