using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Models;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Entities;
using PhotoApp.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, UserModel>, IUserRepository<UserEntity, UserModel>
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserRepository(ApplicationDbContext applicationDbContext, ILogger logger
            , IMapper mapper, UserManager<UserEntity> userManager) 
            : base(applicationDbContext, logger, mapper) 
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException("ApplicationDbContext cannot be null.");
            }
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // Override
        public override async Task<IEnumerable<UserModel>> All()
        {
            try
            {
                var userEntities = await this.dbSet.ToListAsync().ConfigureAwait(false);
                return this._mapper.Map<IEnumerable<UserModel>>(userEntities);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<UserModel>();
            }
        }

        public override async Task<bool> Update(UserModel userModel)
        {
            try
            {
                var existingUser = await this.dbSet.Where(x => x.Id == userModel.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(userModel);

                existingUser.UserName = userModel.UserName;
                existingUser.FirstName = userModel.FirstName;
                existingUser.LastName = userModel.LastName;
                existingUser.Email = userModel.Email;

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} Update function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var exist = await this.dbSet.Where(x => x.Id == Id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                this.dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UserModel>> RegisterUserAsync(UserModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Reigster Model is null");
            }

            try
            {
                UserEntity userEntity = this._mapper.Map<UserEntity>(model);
                System.Console.WriteLine(userEntity);
                IdentityResult identityResult = await this._userManager.CreateAsync(userEntity, model.Password);
                Response<UserModel> response = new Response<UserModel>();
                if (identityResult.Succeeded)
                {
                    response.Success = true;
                    response.Message = "thành công";
                    return response;
                }
                response.Success = false;
                response.Message = identityResult.Errors.ToString();
                return response;
            }
            catch (Exception ex)
            {
                //this._unitOfWork.Dispose();
                this._logger.LogError($"Can't register account, Error Message = {ex.Message}");
                throw;
            }
        }
    }
}
