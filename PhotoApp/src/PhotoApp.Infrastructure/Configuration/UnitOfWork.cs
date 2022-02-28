
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Models;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Entities;
using PhotoApp.Infrastructure.Repositories;

namespace PhotoApp.Infrastructure.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public IUserRepository<UserEntity, UserModel> userRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory
            , IMapper mapper, UserManager<UserEntity> userManager)
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException("Context argument cannot be null in UnitOfWork.");
            }

            this._applicationDbContext = applicationDbContext;
            this._logger = loggerFactory.CreateLogger("logs");
            this._mapper = mapper;
            this._userManager = userManager;

            this.userRepository = new UserRepository(applicationDbContext, this._logger, this._mapper, this._userManager);
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
