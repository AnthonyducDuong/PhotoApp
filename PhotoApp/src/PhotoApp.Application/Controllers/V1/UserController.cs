using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Wrappers;

namespace PhotoApp.Application.Controllers.V1
{
    [Authorize]
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Update information
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(BasicInfoUserRequest request)
        {
            try
            {
                var response = await this._unitOfWork.userRepository.UpdateBasicInformationAsync(request);

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
        /// - Warning: change email require to relogin because refresh and access token have claim email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("changeemail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
        {
            try
            {
                var response = await this._unitOfWork.userRepository.ChangeEmailAsync(request);
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
    }
}
