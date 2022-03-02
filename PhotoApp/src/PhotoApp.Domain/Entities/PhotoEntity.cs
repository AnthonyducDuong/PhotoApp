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
        public IList<LikePhotoEntity>? likePhotoEntities { get; set; }

        public IList<DislikePhotoEntity>? dislikePhotoEntities { get; set; }

        public ICollection<CommentEntity>? commentEntities { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }
        public UserEntity? userEntity { get; set; }
    }
}
