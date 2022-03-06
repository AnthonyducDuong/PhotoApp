using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Entities
{
    public class DislikePhotoEntity
    {
        private UserEntity? _userEntity;
        private PhotoEntity? _photoEntity;

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
        public Guid PhotoId { get; set; }
        public PhotoEntity photoEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => this._lazyLoader.Load(this, ref this._photoEntity);
#pragma warning restore CS8603 // Possible null reference return.
            set => this._photoEntity = value;
        }
    }
}
