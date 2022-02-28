using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Interfaces.IServices;
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
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public UserRepository(ApplicationDbContext applicationDbContext, ILogger logger
            , IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration, IMailService mailService) 
            : base(applicationDbContext, logger, mapper) 
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException("ApplicationDbContext cannot be null.");
            }
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
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
            Response<UserModel> response = new Response<UserModel>();
            if (model == null)
            {
                throw new NullReferenceException("Reigster Model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                response.Success = false;
                response.Message = "Confirm password doesn't match password";
                response.Data = model;
            }

            try
            {
                // Map model to entity => to add in database
                UserEntity userEntity = this._mapper.Map<UserEntity>(model);

                // User IUserManager in Identity -- hash password
                IdentityResult createUserResult = await this._userManager.CreateAsync(userEntity, model.Password);
                IdentityResult addToRoleResult = await this._userManager.AddToRoleAsync(userEntity, RoleConstants.ROLE_USER);

                if (createUserResult.Succeeded)
                {
                    /* response.Success = true;
                     response.Message = "Success";
                     response.Data = model;
                     return response;*/

                    // handle send mail confirm
                    var confirmEmailToken = await this._userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                    
                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    string url = $"{this._configuration["AppUrl"]}/api/auth/confirmemail?userid={userEntity.Id}&token={validEmailToken}";

                    await this._mailService.SendEmailAsync(model.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                    response.Success = true;
                    response.Message = "Success";
                    response.Data = model;
                    return response;
                }

                // Use stringbuilder -- Memory heap
                StringBuilder errors = new StringBuilder();
                foreach (var item in createUserResult.Errors)
                {
                    errors.Append(item.Code.ToString());
                    errors.Append(": ");
                    errors.Append(item.Description.ToString());
                }

                response.Success = false;
                response.Message = errors.ToString();
                return response;
            }
            catch (Exception ex)
            {
                //this._unitOfWork.Dispose();
                this._logger.LogError($"Can't register account, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<Response<UserModel>> ConfirmEmailAsync(string UserId, string Token)
        {
            UserEntity userEntity = await this._userManager.FindByIdAsync(UserId);
             
            if (userEntity == null)
            {
                return new Response<UserModel>()
                {
                    Success = false,
                    Message = "User not existed in system",
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            IdentityResult confirmEmailResult = await this._userManager.ConfirmEmailAsync(userEntity, normalToken);

            if (confirmEmailResult.Succeeded)
            {
                return new Response<UserModel>()
                {
                    Success = true,
                    Message = "Email confirmed successfully",
                };
            }

            return new Response<UserModel>()
            {
                Success = false,
                Message = "Can't confirm email",
                Errors = confirmEmailResult.Errors.Select(s => s.Description),
            };
        }
    }
}
