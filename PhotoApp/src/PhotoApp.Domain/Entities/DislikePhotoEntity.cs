using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Entities
{
    public class DislikePhotoEntity
    {
        // Foreign Key
        // User Entity
        public Guid UserId { get; set; }
        public virtual UserEntity? userEntity { get; set; }

        // Photo Entity
        public Guid PhotoId { get; set; }
        public virtual PhotoEntity? photoEntity { get; set; }
    }
}
