using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Models;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Configuration;

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

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await this._unitOfWork.userRepository.RegisterUserAsync(userModel);
                await this._unitOfWork.CompleteAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                Response<UserModel> response = new Response<UserModel> { Success = false, Message = ex.Message };
                return BadRequest(response);
            }
        }

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
                    return Redirect($"{ this._configuration["AppUrl"]}/abc.html");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserModel>()
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }
}
