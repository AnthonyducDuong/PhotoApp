using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PhotoApp.Domain.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        /* [Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Required(ErrorMessage = "UserId is invalid")]
public Guid Id { get; init; }*/

        [Required(ErrorMessage = "First name is invalid")]
        [StringLength(50, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is invalid")]
        [StringLength(20, ErrorMessage = "Your First Name can contain only 20 characters")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        /*[Required(ErrorMessage = "Email is invalid")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }*/

        /*[Required(ErrorMessage = "Password is invalid")]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        public string? Password { get; set; }*/

        // Relationship
        public virtual ICollection<PhotoEntity>? photoEntities { get; set; }

        public virtual IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public virtual IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public virtual ICollection<CommentEntity>? commentEntities { get; set; }

        public virtual IList<LikeCommentEntity>? likeCommentEntities { get; set; }

        public virtual IList<DislikeCommentEntity>? dislikeCommentEntities { get; set; }

        // Foreign Key
        /*public int RoleId { get; set; }
        public virtual RoleEntity? RoleEntity { get; set; }*/
        public UserEntity() { }

        public UserEntity(Guid Id, string? firstName, string? lastName, string? gender, string? username, string email)
        {
            this.Id = Id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.UserName = username;
            this.Email = email;
        }
    }
}
