using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;

        public IUserRepository userRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory)
        {
            this._applicationDbContext = applicationDbContext;
            this._logger = loggerFactory.CreateLogger("logs");

            this.userRepository = new UserRepository(applicationDbContext, this._logger);
        }

        public async Task CompleteAsync()
        {
            await this._applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._applicationDbContext.Dispose();
        }
    }
}
