using Microsoft.EntityFrameworkCore;
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
        /* public virtual DbSet<PhotoEntity> Photos { get; set; }*/
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
        }
    }
}
