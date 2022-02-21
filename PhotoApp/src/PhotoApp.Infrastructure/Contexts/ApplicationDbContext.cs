using Microsoft.EntityFrameworkCore;
using PhotoApp.Domain.Enums;
using PhotoApp.Infrastructure.Constants;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Contexts
{
    public  class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region DbSet
        // Role Entity
        public virtual DbSet<RoleEntity> RoleEntities { get; set; }

        // User Entity
        public virtual DbSet<UserEntity> UserEntities { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<PhotoEntity>(entity =>
             {
                 entity.ToTable("Photo");
                 entity.HasKey(p => p.Id);
                 entity.Property(p => p.Image).IsRequired();
                 entity.Property(p => p.Created).HasDefaultValueSql("getutcdate()");
                 entity.Property(p => p.Updated).HasDefaultValueSql("getutcdate()");
                 entity.Property(p => p.Like).HasDefaultValue(0);
                 entity.Property(p => p.Dislike).HasDefaultValue(0);
             });*/

            // Role Entity
            modelBuilder.Entity<RoleEntity>(entity =>
            {

                entity.ToTable("Role");
                entity.HasKey(k => k.Id);
            });

            // User Entity
            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");
                entity.HasKey(k => k.Id);
                entity.HasOne<RoleEntity>(r => r.RoleEntity)
                      .WithMany(u => u.userEntities)
                      .HasForeignKey(r => r.RoleId)
                      .HasConstraintName("Fk_User_Role");
            });
        }
    }
}
