
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
using PhotoApp.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        // Get user by email
        public Task<UserEntity> GetUserByEmail(string email);

        // Register for role user
        public Task<Response<RegisterResponse>> RegisterUserAsync(RegisterRequest request);

        // Confirm email
        public Task<Response<AuthenticateResponse>> ConfirmEmailAsync(string UserId, string Token);

        // Login
        public Task<Response<AuthenticateResponse>> LoginAsync(AuthenticateRequest request);

        // Refresh token - return new access token
        public Task<Response<RefreshTokenResponse>> RefreshNewTokenAsync(string accessToken);

        // Forget password
        public Task<NormalResponse> ForgetPasswordAsync(string email);

        // Reset password
        public Task<NormalResponse> ResetPasswordAsync(ResetPasswordRequest request);

        // Update information
        public Task<NormalResponse> UpdateBasicInformationAsync(BasicInfoUserRequest request);

        // Change Email
        public Task<NormalResponse> ChangeEmailAsync(ChangeEmailRequest request);
    }
}
