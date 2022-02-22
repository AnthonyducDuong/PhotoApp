using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "UserId is invalid")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "First name is invalid")]
        [StringLength(50, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is invalid")]
        [StringLength(20, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        [Required(ErrorMessage = "Email is invalid")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is invalid")]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        public string? Password { get; set; }

        // Relationship
        public ICollection<PhotoEntity>? photoEntities { get; set; }

        public IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public ICollection<CommentEntity>? commentEntities { get; set; }

        public IList<LikeCommentEntity>? likeCommentEntities { get; set; }

        public IList<DislikeCommentEntity>? dislikeCommentEntities { get; set; }

        // Foreign Key
        public int RoleId { get; set; }
        public RoleEntity? RoleEntity { get; set; }
    }
}
