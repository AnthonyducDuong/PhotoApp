using Microsoft.AspNetCore.Identity;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Contexts
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is invalid")]
        [StringLength(50, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is invalid")]
        [StringLength(20, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        // Relationship
        public ICollection<PhotoEntity>? photoEntities { get; set; }

        public IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public ICollection<CommentEntity>? commentEntities { get; set; }

        public IList<LikeCommentEntity>? likeCommentEntities { get; set; }

        public IList<DislikeCommentEntity>? dislikeCommentEntities { get; set; }

        // Foreign Key
       /* public int RoleId { get; set; }
        public RoleEntity? RoleEntity { get; set; }*/
    }
}
