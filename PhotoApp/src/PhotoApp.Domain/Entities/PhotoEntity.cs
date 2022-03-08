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
        public virtual IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public virtual IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public virtual ICollection<CommentEntity>? commentEntities { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }
        public virtual UserEntity? userEntity { get; set; }


        public PhotoEntity() { }

        public PhotoEntity(Guid id, string description, string url, ModePhotoEnums mode, DateTime createdAt, DateTime updated, IList<LikePhotoEntity> likePhotoEntities,
           IList<DislikePhotoEntity> dislikePhotoEntities, ICollection<CommentEntity> commentEntities)
        {
            this.Id = id;
            this.Description = description;
            this.Url = url;
            this.Mode = mode;
            this.CreatedAt = createdAt;
            this.Updated = updated;
            this.likePhotoEntities = likePhotoEntities;
            this.dislikePhotoEntities = dislikePhotoEntities;
            this.commentEntities = commentEntities;
        }

        public PhotoEntity(Guid id, string? description, string? url, ModePhotoEnums mode, DateTime createdAt, DateTime updated, 
            IList<LikePhotoEntity>? likePhotoEntities, IList<DislikePhotoEntity>? dislikePhotoEntities, ICollection<CommentEntity>? commentEntities, 
            UserEntity? userEntity)
        {
            this.Id = id;
            this.Description = description;
            this.Url = url;
            this.Mode = mode;
            this.CreatedAt = createdAt;
            this.Updated = updated;
            this.likePhotoEntities = likePhotoEntities;
            this.dislikePhotoEntities = dislikePhotoEntities;
            this.commentEntities = commentEntities;
            this.userEntity = userEntity;
        }
    }
}
