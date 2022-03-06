using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Interfaces.IServices;
using PhotoApp.Domain.Services;
using PhotoApp.Infrastructure.Configuration;
using PhotoApp.Infrastructure.Contexts;
using System.Text;
using System.Text.Json.Serialization;

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

            // Ignoring circular references
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Entity Framework
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

            // For Identity
            services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // Adding Jwt Bearer
                .AddJwtBearer(options => {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = configuration["Token:ValidAudience"],
                        ValidIssuer = configuration["Token:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SceretKey"])),
                        ClockSkew = TimeSpan.Zero,
                    };
                    
                })
                .AddCookie();

            /*services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IUserService, UserService>();

            // SendGrid Mail service
            services.AddTransient<IMailService, MailService>();

            // Jwt service
            services.AddTransient<IJwtService, JwtService>();

            services.AddHttpClient();

            return services;
        }
    }
}
