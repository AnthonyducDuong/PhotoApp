using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Configuration;
using System.Text;

namespace PhotoApp.Application.Controllers.V1
{
    [Authorize]
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }

        /// <summary>
        /// Register new account for user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>RegisterResponse</returns>
        /// <remarks>
        /// - https://localhost:7109/api/photoappservice/v1/auth/register
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody]RegisterRequest request )
        {
            if (!ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    sb.Append(item);
                    sb.Append("& ");
                }
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = sb.ToString(),
                });
            }

            try
            {
                var response = await this._unitOfWork.userRepository.RegisterUserAsync(request);
                
                if (response.Success)
                {
                    await this._unitOfWork.CompleteAsync();
                    return Ok(response);
                }
                this._unitOfWork.Dispose();
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                this._unitOfWork.Dispose();
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Confirm email
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="token"></param>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/auth/confirmemail</remarks>
        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string UserId, string token)
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            try
            {
                var response = await this._unitOfWork.userRepository.ConfirmEmailAsync(UserId, token);

                if (response.Success)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    HttpContext.Response.Cookies.Append("AccessToken", response.Data.AccessToken, new CookieOptions { HttpOnly = true });
                    HttpContext.Response.Cookies.Append("RefreshToken", response.Data.RefreshToken, new CookieOptions { HttpOnly = true });
#pragma warning restore CS8604 // Possible null reference argument.
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>()
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Login for user or admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Data of user; AccessToken and RefreshToken</returns>
        /// <remarks>
        /// - https://localhost:7109/api/photoappservice/v1/auth/login
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(AuthenticateRequest request)
        {
            // not run to this ??
            if (!ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    sb.Append(item);
                    sb.Append("& ");
                }
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = sb.ToString(),
                });
            }

            try
            {
                var response = await this._unitOfWork.userRepository.LoginAsync(request);
                await this._unitOfWork.CompleteAsync();
                if (response.Success)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    HttpContext.Response.Cookies.Append("AccessToken", response.Data.AccessToken, new CookieOptions { HttpOnly = true });
                    HttpContext.Response.Cookies.Append("RefreshToken", response.Data.RefreshToken, new CookieOptions { HttpOnly = true });
#pragma warning restore CS8604 // Possible null reference argument.
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns>New AccessToken</returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/auth/refreshtoken</remarks>
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshNewToken([FromBody] RefreshTokenRequest request)
        {
            string accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var response = await this._unitOfWork.userRepository.RefreshNewTokenAsync(accessToken, request.refreshToken);
#pragma warning restore CS8604 // Possible null reference argument.

                // Delete access token in cookies
                if (Request.Cookies["AccessToken"] != null)
                {
                    Response.Cookies.Delete("AccessToken");
                }

                if (response.Success)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    HttpContext.Response.Cookies.Append("AccessToken", response.Data.accessToken, new CookieOptions { HttpOnly = true });
#pragma warning restore CS8604 // Possible null reference argument.
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Forget password
        /// </summary>
        /// <returns>Send email with token reset mail and email</returns>
        /// <remarks>https://localhost:7109/api/photoappservice/v1/auth/forgetpassword</remarks>
        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var response = await this._unitOfWork.userRepository.ForgetPasswordAsync(email);

                    if (response.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response);
                    }
                }
                else
                {
                    return BadRequest(new NormalResponse
                    {
                        Success = false,
                        Message = "Email is null or empty",
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>https://localhost:7109/api/photoappservice/v1/auth/resetpassword</remarks>
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    sb.Append(item);
                    sb.Append("& ");
                }
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = sb.ToString(),
                });
            }

            try
            {
                var response = await this._unitOfWork.userRepository.ResetPasswordAsync(request);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }
}
