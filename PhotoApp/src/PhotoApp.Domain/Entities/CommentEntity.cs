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
        public virtual ICollection<CommentEntity>? commentEntities { get; set; }

        public virtual IList<LikeCommentEntity>? likeCommentEntities { get; set; }

        public virtual IList<DislikeCommentEntity>? dislikeCommentEntities { get; set; }

        // Foreign Key
        public Guid UserID { get; set; }
        public virtual UserEntity? userEntity { get; set; }

        public Guid PhotoId { get; set; }
        public virtual PhotoEntity? photoEntity { get; set; }

        public Guid CommentId { get; set; }
        public virtual CommentEntity? commentEntity { get; set; }
    }
}
