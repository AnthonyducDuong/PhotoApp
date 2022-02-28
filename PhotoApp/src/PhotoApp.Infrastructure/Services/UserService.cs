using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Models;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Configuration;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Services
{
    public class UserService //: IUserService
    {
        /*private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<bool> Add(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> All()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> Find(Expression<Func<UserModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel?> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UserModel>> RegisterUserAsync(UserModel userModel)
        {
            try
            {
                return await this._unitOfWork.userRepository.RegisterUserAsync(userModel);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't register, Error Message = {ex.Message}");
                throw;
            }
        }

        public Task<bool> Update(UserModel model)
        {
            throw new NotImplementedException();
        }*/
    }
}
