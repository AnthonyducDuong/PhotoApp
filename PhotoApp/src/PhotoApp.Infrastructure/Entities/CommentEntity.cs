using PhotoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Entities
{
    public class CommentEntity
    {
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
        public ICollection<CommentEntity>? commentEntities { get; set; }

        public IList<LikeCommentEntity>? likeCommentEntities { get; set; }

        public IList<DislikeCommentEntity>? dislikeCommentEntities { get; set; }

        // Foreign Key
        public Guid UserID { get; set; }
        public UserEntity? userEntity { get; set; }

        public Guid PhotoId { get; set; }
        public PhotoEntity? photoEntity { get; set; }

        public Guid CommentId { get; set; }
        public CommentEntity? commentEntity { get; set; }
    }
}
