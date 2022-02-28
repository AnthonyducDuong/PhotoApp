using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoApp.Domain.Enums;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region DbSet
        // Role Entity
       /* public virtual DbSet<RoleEntity>? RoleEntities { get; set; }*/

        // User Entity
        /*public virtual DbSet<UserEntity>? UserEntities { get; set; }*/
        
        // Photo Entity
        public virtual DbSet<PhotoEntity>? PhotoEntities { get; set; }

        // LikePhoto Entity
        public virtual DbSet<LikePhotoEntity>? LikePhotoEntities { get; set; }

        // DislikePhoto Entity
        public virtual DbSet<DislikePhotoEntity>? DislikePhotoEntities { get; set; }

        // Comment Entity
        public virtual DbSet<CommentEntity>? CommentEntities { get; set; }

        // LikeComment Entity
        public virtual DbSet<LikeCommentEntity>? LikeCommentEntities { get; set;}

        //DislikeComment Entity
        public virtual DbSet<DislikeCommentEntity>? DislikeCommentEntities { get; set;}
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // override identity model
            base.OnModelCreating(modelBuilder);

            // Role Entity
            /*modelBuilder.Entity<RoleEntity>(entity =>
            {

                entity.ToTable("Role");
                entity.HasKey(k => k.Id);
            });*/

            // User Entity
            /*modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");
                entity.HasKey(k => k.Id);
                entity.HasOne<RoleEntity>(r => r.RoleEntity)
                      .WithMany(u => u.userEntities)
                      .HasForeignKey(r => r.RoleId)
                      .HasConstraintName("Fk_User_Role");
            });*/

            // Remove the "Aspnet" prefix from all of the primary tables
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
 
            // Photo Entity
            modelBuilder.Entity<PhotoEntity>(entity => {
                entity.ToTable("Photo");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Url).IsRequired();
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
                entity.Property(p => p.Updated).HasDefaultValueSql("getutcdate()");
                entity.HasOne<UserEntity>(u => u.userEntity)
                      .WithMany(p => p.photoEntities)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("Fk_Photo_User");
            });
            
            // LikePhoto Entity
            modelBuilder.Entity<LikePhotoEntity>(entity => {
                entity.ToTable("LikePhoto");
                entity.HasKey(l => new { l.UserId, l.PhotoId });
                // Photo Entity
                entity.HasOne<PhotoEntity>(p => p.PhotoEntity)
                      .WithMany(l => l.likePhotoEntities)
                      .HasForeignKey(p => p.PhotoId)
                      .HasConstraintName("Fk_LikePhoto_Photo")
                      .OnDelete(DeleteBehavior.NoAction);
                // User Entity
                entity.HasOne<UserEntity>(u => u.UserEntity)
                      .WithMany(l => l.likePhotoEntities)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("Fk_LikePhoto_User")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // DislikePhoto Entity
            modelBuilder.Entity<DislikePhotoEntity>(entity => {
                entity.ToTable("DislikePhoto");
                entity.HasKey(d => new { d.UserId, d.PhotoId });
                // Photo Entity
                entity.HasOne<PhotoEntity>(p => p.PhotoEntity)
                      .WithMany(d => d.dislikePhotoEntities)
                      .HasForeignKey(p => p.PhotoId)
                      .HasConstraintName("Fk_DislikePhoto_Photo")
                      .OnDelete(DeleteBehavior.NoAction);
                // User Entity
                entity.HasOne<UserEntity>(u => u.UserEntity)
                      .WithMany(d => d.dislikePhotoEntities)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("Fk_DislikePhoto_User")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Comment Entity
            modelBuilder.Entity<CommentEntity>(entity => {
                entity.ToTable("Comment");
                entity.HasKey(c => c.Id);
                // User Entity
                entity.HasOne<UserEntity>(u => u.userEntity)
                      .WithMany(c => c.commentEntities)
                      .HasForeignKey(u => u.UserID)
                      .HasConstraintName("Fk_Comment_User")
                      .OnDelete(DeleteBehavior.NoAction);
                // Photo Entity
                entity.HasOne<PhotoEntity>(p => p.photoEntity)
                      .WithMany(c => c.commentEntities)
                      .HasForeignKey(p => p.PhotoId)
                      .HasConstraintName("Fk_Comment_Photo")
                      .OnDelete(DeleteBehavior.NoAction);
                // Comment Entity
                entity.HasOne<CommentEntity>(ce => ce.commentEntity)
                      .WithMany(c => c.commentEntities)
                      .HasForeignKey(ce => ce.CommentId)
                      .HasConstraintName("Fk_Comment_Comment")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // LikeComment Entity
            modelBuilder.Entity<LikeCommentEntity>(entity => {
                entity.ToTable("LikeComment");
                entity.HasKey(l => new { l.UserId, l.CommentId });
                // Comment Entity
                entity.HasOne<CommentEntity>(c => c.CommentEntity)
                      .WithMany(l => l.likeCommentEntities)
                      .HasForeignKey(c => c.CommentId)
                      .HasConstraintName("Fk_LikeComment_Comment")
                      .OnDelete(DeleteBehavior.NoAction);
                // User Entity
                entity.HasOne<UserEntity>(u => u.UserEntity)
                      .WithMany(l => l.likeCommentEntities)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("Fk_LikeComment_User")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // DislikeComment Entity
            modelBuilder.Entity<DislikeCommentEntity>(entity => {
                entity.ToTable("DislikeComment");
                entity.HasKey(l => new { l.UserId, l.CommentId });
                // Comment Entity
                entity.HasOne<CommentEntity>(c => c.CommentEntity)
                      .WithMany(d => d.dislikeCommentEntities)
                      .HasForeignKey(c => c.CommentId)
                      .HasConstraintName("Fk_DislikeComment_Comment")
                      .OnDelete(DeleteBehavior.NoAction);
                // User Entity
                entity.HasOne<UserEntity>(u => u.UserEntity)
                      .WithMany(d => d.dislikeCommentEntities)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("Fk_DislikeComment_User")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Create seed data for role entity
            RoleDbInitializer.Seed(modelBuilder);
        }
    }
}
