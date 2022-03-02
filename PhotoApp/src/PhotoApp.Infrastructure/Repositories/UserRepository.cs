using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Interfaces.IServices;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserRepository(ApplicationDbContext applicationDbContext, ILogger logger
               , UserManager<UserEntity> userManager, IConfiguration configuration
            , IMailService mailService, IMapper mapper, IJwtService jwtService) 
            : base(applicationDbContext, logger, mapper) 
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException("ApplicationDbContext cannot be null.");
            }
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        // Override
        public override async Task<IEnumerable<UserEntity>> All()
        {
            try
            {
                return await this.dbSet.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<UserEntity>();
            }
        }

        public override async Task<bool> Update(UserEntity userEntity)
        {
            try
            {
                var existingUser = await this.dbSet.Where(x => x.Id == userEntity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(userEntity);

                existingUser.UserName = userEntity.UserName;
                existingUser.FirstName = userEntity.FirstName;
                existingUser.LastName = userEntity.LastName;
                existingUser.Email = userEntity.Email;

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

        public Task<UserEntity> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<RegisterResponse>> RegisterUserAsync(RegisterRequest request)
        {
            Response<RegisterResponse> response = new Response<RegisterResponse>();
            if (request == null)
            {
                throw new NullReferenceException("User entity is null");
            }

            if (request.Password != request.ConfirmPassword)
            {
                response.Success = false;
                response.Message = "Confirm password doesn't match password";
            }

            // map request to user entity
            UserEntity userEntity = this._mapper.Map<UserEntity>(request);

            try
            {
                // User IUserManager in Identity -- hash password
                IdentityResult createUserResult = await this._userManager.CreateAsync(userEntity, request.Password);
                IdentityResult addToRoleResult = await this._userManager.AddToRoleAsync(userEntity, RoleConstants.ROLE_USER);

                if (createUserResult.Succeeded)
                {
                    // handle send mail confirm
                    var confirmEmailToken = await this._userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                    
                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    string url = $"{this._configuration["AppUrl"]}/{ApiConstants.ServiceName}/v1/auth/confirmemail?userid={userEntity.Id}&token={validEmailToken}";

#pragma warning disable CS8604 // Possible null reference argument.
                    await this._mailService.SendEmailAsync(userEntity.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
#pragma warning restore CS8604 // Possible null reference argument.

                    response.Success = true;
                    response.Message = "Send mail confirm success";
                    response.Data = new RegisterResponse
                    {
                        UserName = userEntity.UserName,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Email = userEntity.Email,
                        Role = RoleConstants.ROLE_USER,
                        IsVerified = false,
                    };
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
                this._logger.LogError($"Can't register account, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<Response<AuthenticateResponse>> ConfirmEmailAsync(string UserId, string Token)
        {
            UserEntity userEntity = await this._userManager.FindByIdAsync(UserId);
             
            if (userEntity == null)
            {
                return new Response<AuthenticateResponse>()
                {
                    Success = false,
                    Message = "User not existed in system",
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            try
            {
                // Confirm email
                IdentityResult confirmEmailResult = await this._userManager.ConfirmEmailAsync(userEntity, normalToken);

                if (confirmEmailResult.Succeeded)
                {
                    // role user but need check
                    int flag = 0; // role user
                    if (await this._userManager.IsInRoleAsync(userEntity, RoleConstants.ROLE_ADMIN))
                    {
                        flag = 1;
                    }

                    return new Response<AuthenticateResponse>()
                    {
                        Success = true,
                        Message = "Email confirmed successfully",
                        Data = new AuthenticateResponse
                        {
                            Id = userEntity.Id,
                            UserName = userEntity.UserName,
                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            Email = userEntity.Email,
                            Role = flag == 0 ? RoleConstants.ROLE_USER : RoleConstants.ROLE_ADMIN,
                            IsVerified = true,
                            AccessToken = this._jwtService.GenerateAccessToken(userEntity),
                            RefreshToken = this._jwtService.GenerateRefreshToken(userEntity.Email),
                        }
                    };
                }

                return new Response<AuthenticateResponse>()
                {
                    Success = false,
                    Message = "Can't confirm email",
                    Errors = confirmEmailResult.Errors.Select(s => s.Description),
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't confirm email, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<Response<AuthenticateResponse>> LoginAsync(AuthenticateRequest request)
        {
            UserEntity userEntity = await this._userManager.FindByNameAsync(request.UserName);

            if (userEntity == null)
            {
                return new Response<AuthenticateResponse>()
                {
                    Success = false,
                    Message = "User not existed in system",
                };
            }

            try
            {
                // Check password
                var checkPasswordResult = await this._userManager.CheckPasswordAsync(userEntity, request.Password);

                if (checkPasswordResult)
                {
                    int flag = 0; // role user
                    if (await this._userManager.IsInRoleAsync(userEntity, RoleConstants.ROLE_ADMIN))
                    {
                        flag = 1;
                    }

                    return new Response<AuthenticateResponse>()
                    {
                        Success = true,
                        Message = "Login successfully",
                        Data = new AuthenticateResponse
                        {
                            Id = userEntity.Id,
                            UserName = userEntity.UserName,
                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            Email = userEntity.Email,
                            Role = flag == 0 ? RoleConstants.ROLE_USER : RoleConstants.ROLE_ADMIN,
                            IsVerified = true,
                            AccessToken = this._jwtService.GenerateAccessToken(userEntity),
                            RefreshToken = this._jwtService.GenerateRefreshToken(userEntity.Email),
                        }
                    };
                }

                return new Response<AuthenticateResponse>()
                {
                    Success = false,
                    Message = "Invalid Password",
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't login, Error Message = {ex.Message}");
                throw;
            }
        }
    }
}
