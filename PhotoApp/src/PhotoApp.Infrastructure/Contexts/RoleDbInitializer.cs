using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Contexts
{
    internal class RoleDbInitializer
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = RoleConstants.ROLE_ADMIN,
                    ConcurrencyStamp = "1",
                    NormalizedName = "Admin",
                },
                new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = RoleConstants.ROLE_USER,
                    ConcurrencyStamp = "2",
                    NormalizedName = "User",
                }
            );
        }
    }
}
