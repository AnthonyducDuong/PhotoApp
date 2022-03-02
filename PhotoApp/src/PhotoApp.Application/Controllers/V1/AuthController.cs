using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Configuration;
using System.Text;

namespace PhotoApp.Application.Controllers.V1
{
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
        /// <returns></returns>
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
                await this._unitOfWork.CompleteAsync();
                if (response.Success)
                {
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
        /// Confirm email
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]AuthenticateRequest request)
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
    }
}
