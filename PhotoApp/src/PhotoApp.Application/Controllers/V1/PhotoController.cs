using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Wrappers;
using System.Text;

namespace PhotoApp.Application.Controllers.V1
{
    [Authorize]
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class PhotoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// User create new photo
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Photo Entity</returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/photo</remarks>
        [HttpPost]
        public async Task<IActionResult> createPhotoAsync([FromBody] PhotoRequest request)
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
                var response = await this._unitOfWork.photoRepository.createPhotoAsync(request);

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

        [HttpPost("getAll")]
        public async Task<IActionResult> getAllPhotosAsync(EmailUserRequest request)
        {
            try
            {
                var response = await this._unitOfWork.photoRepository.getAllPhotosOfUser(request.Email);

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
