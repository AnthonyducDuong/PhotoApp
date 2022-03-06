
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Interfaces.IServices;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Repositories;

namespace PhotoApp.Infrastructure.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IJwtService _jwtService;

        public IUserRepository userRepository { get; private set; }

        public IPhotoRepository photoRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory
            , IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration
            , IMailService mailService, IJwtService jwtService)
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException("Context argument cannot be null in UnitOfWork.");
            }

            this._applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this._logger = loggerFactory.CreateLogger("logs");
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            this._jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            this.userRepository = new UserRepository(applicationDbContext, this._logger, 
                this._userManager, this._configuration, this._mailService, this._mapper, this._jwtService);

            this.photoRepository = new PhotoRepository(applicationDbContext, this._logger, this._mapper, this._userManager);
        }


        public async Task CompleteAsync()
        {
            await this._applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (this._applicationDbContext != null)
            {
                this._applicationDbContext.Dispose();
            }
        }
    }
}
