using Microsoft.AspNetCore.Builder;
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
                if (!context.RoleEntities.Any())
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                {
                    context.RoleEntities.AddRange(new RoleEntity()
                    {
                        Type = RoleConstants.ROLE_ADMIN,
                    },
                    new RoleEntity()
                    {
                        Type = RoleConstants.ROLE_USER,
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
