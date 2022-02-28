using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Interfaces.IServices;
using PhotoApp.Domain.Models;
using PhotoApp.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Services
{
    public class UserService //: IUserService
    {
        /*public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
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
