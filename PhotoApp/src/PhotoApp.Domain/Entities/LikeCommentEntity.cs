using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Entities
{
    public class LikeCommentEntity
    {
        private UserEntity? _userEntity;
        private CommentEntity? _commentEntity;

        private ILazyLoader? _lazyLoader { get; set; }
        // Foreign Key
        // User Entity
        public Guid UserId { get; set; }
        public UserEntity userEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._userEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._userEntity = value;
        }

        // Photo Entity
        public Guid CommentId { get; set; }
        public CommentEntity commentEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._commentEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._commentEntity = value;
        }
    }
}
