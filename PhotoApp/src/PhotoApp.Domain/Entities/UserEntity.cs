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
        private ICollection<PhotoEntity>? _photoEntities;
        private IList<LikePhotoEntity>? _likePhotoEntities;
        private IList<DislikePhotoEntity>? _dislikePhotoEntities;
        private ICollection<CommentEntity>? _commentEntities;
        private IList<LikeCommentEntity>? _likeCommentEntities;
        private IList<DislikeCommentEntity>? _dislikeCommentEntities;
        private ILazyLoader _lazyLoader { get; set; }

        public UserEntity(ILazyLoader lazyLoader)
        {
            this._lazyLoader = lazyLoader;
        }

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
        public ICollection<PhotoEntity>? photoEntities 
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._photoEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._photoEntities = value;
        }

        public IList<LikePhotoEntity>? likePhotoEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._likePhotoEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._likePhotoEntities = value;
        }

        public IList<DislikePhotoEntity>? dislikePhotoEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._dislikePhotoEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._dislikePhotoEntities = value;
        }

        public ICollection<CommentEntity>? commentEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._commentEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._commentEntities = value;
        }

        public IList<LikeCommentEntity>? likeCommentEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._likeCommentEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._likeCommentEntities = value;
        }

        public IList<DislikeCommentEntity>? dislikeCommentEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._dislikeCommentEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._dislikeCommentEntities = value;
        }

        // Foreign Key
        /*public int RoleId { get; set; }
        public RoleEntity? RoleEntity { get; set; }*/
    }
}
