/*using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PhotoApp.Infrastructure.Constants;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Contexts
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new RoleEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleConstants.ROLE_ADMIN,
                        ConcurrencyStamp = "1",
                        NormalizedName = "Admin",
                    },
                    new RoleEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleConstants.ROLE_USER,
                        ConcurrencyStamp = "2",
                        NormalizedName = "User",
                    });

                    context.SaveChanges();
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}
*/