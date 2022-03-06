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
    public class PhotoEntity
    {
        private IList<LikePhotoEntity>? _likePhotoEntities;
        private IList<DislikePhotoEntity>? _dislikePhotoEntities;
        private ICollection<CommentEntity>? _commentEntities;
        private UserEntity? _userEntity;

        private ILazyLoader _lazyLoader { get; set; }

        public PhotoEntity(ILazyLoader lazyLoader)
        {
            this._lazyLoader = lazyLoader;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "PhotoId is invalid")]
        public Guid Id { get; init; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Photo URL is invalid")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Mode is invalid")]
        public ModePhotoEnums Mode { get; set; }

        [Required(ErrorMessage = "CreatedAt is invalid")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated is invalid")]
        public DateTime Updated { get; set; }

        // Relationship
        /*public IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public ICollection<CommentEntity>? commentEntities { get; set; }*/

        // Foreign Key
        public Guid UserId { get; set; }
        //public UserEntity? userEntity { get; set; }

        public IList<LikePhotoEntity> likePhotoEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._likePhotoEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._likePhotoEntities = value;
        }

        public IList<DislikePhotoEntity> dislikePhotoEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._dislikePhotoEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._dislikePhotoEntities = value;
        }

        public ICollection<CommentEntity> commentEntities
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._commentEntities);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._commentEntities = value;
        }

        public UserEntity userEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._userEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._userEntity = value;
        }
    }
}
