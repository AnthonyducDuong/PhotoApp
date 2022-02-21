using Microsoft.EntityFrameworkCore;
using PhotoApp.Infrastructure.Contexts;

namespace PhotoApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentException(nameof(configuration));
            }

            services.AddDbContext<ApplicationDbContext>(option => {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            /*errorCodesToAdd: null*/
                            errorNumbersToAdd: null
                        );
                    });
            });

            /*services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();*/

            services.AddHttpClient();

            return services;
        }
    }
}
