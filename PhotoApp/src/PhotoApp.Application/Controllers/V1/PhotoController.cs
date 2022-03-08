using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
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

        /// <summary>
        /// Get photos by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/photo/getphotobyusserid?userId={id}</remarks>
        [HttpGet("getphotosbyuserid")]
        public async Task<IActionResult> getAllPhotosAsync(string userId)
        {
            try
            {
                var response = await this._unitOfWork.photoRepository.getAllPhotosOfUser(userId);

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
        /// Get all photos in system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var result = await this._unitOfWork.photoRepository.All();

                return Ok(new Response<IEnumerable<PhotoEntity>>
                {
                    Success = true,
                    Message = "Get all photos in system successfully!",
                    Data = result,
                });
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
        /// Get photo by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/photo/{Id}</remarks>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                PhotoEntity photoEntity = await this._unitOfWork.photoRepository.GetById(Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (photoEntity != null)
                {
                    return Ok(new Response<PhotoEntity>()
                    {
                        Success = true,
                        Message = "Get photo successfully!",
                        Data = photoEntity,
                    });
                }
                return BadRequest(new Response<PhotoEntity>()
                {
                    Success = false,
                    Message = "Get photo unsuccessfully!",
                });
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

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] PhotoRequest request)
        {
            try
            {
                var response = await this._unitOfWork.photoRepository.updatePhotoAsync(request);

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
    }
}
