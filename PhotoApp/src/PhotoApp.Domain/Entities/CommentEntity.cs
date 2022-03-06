using Microsoft.EntityFrameworkCore.Infrastructure;
using PhotoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Entities
{
    public class CommentEntity
    {
        private ICollection<CommentEntity>? _commentEntities;
        private IList<LikeCommentEntity>? _likeCommentEntities;
        private IList<DislikeCommentEntity>? _dislikeCommentEntities;
        private UserEntity? _userEntity;
        private PhotoEntity? _photoEntity;
        private CommentEntity? _commentEntity;

        private ILazyLoader _lazyLoader { get; set; }

        public CommentEntity(ILazyLoader lazyLoader)
        {
            this._lazyLoader = lazyLoader;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "CommentId is invalid")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Comment is invalid")]
        [DataType(DataType.Text)]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Status is invalid")]
        public StatusCommentEnums Status { get; set; }

        [Required(ErrorMessage = "Date is invalid")]
        public DateTime Date { get; set; }

        // Relationship
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
        public Guid UserID { get; set; }
        public UserEntity? userEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._userEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._userEntity = value;
        }

        public Guid PhotoId { get; set; }
        public PhotoEntity? photoEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._photoEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._photoEntity = value;
        }

        public Guid CommentId { get; set; }
        public CommentEntity? commentEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._commentEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._commentEntity = value;
        }
    }
}
