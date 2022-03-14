using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IConfiguration;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
using PhotoApp.Domain.Wrappers;

namespace PhotoApp.Application.Controllers.V1
{
    [Authorize]
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create new comment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/comment</remarks>
        [HttpPost]
        public async Task<IActionResult> createCommentAsync(CommentRequest request)
        {
            try
            {
                var response = await this._unitOfWork.commentRepository.createCommentAsync(request);

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
        /// Update comment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/photo</remarks>
        [HttpPut]
        public async Task<IActionResult> updateCommentAsync(CommentUpdateRequest request)
        {
            try
            {
                var response = await this._unitOfWork.commentRepository.updateCommentAsync(request);
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
        /// Delete comments tree by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>- https://localhost:7109/api/photoappservice/v1/photo</remarks>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> deleteCommentAsync(string Id)
        {
            try
            {
                var response = await this._unitOfWork.commentRepository.Delete(Id);

                if (response == true)
                {
                    await this._unitOfWork.CompleteAsync();
                    return Ok(new NormalResponse
                    {
                        Success = true,
                        Message = "Delete successfully!",
                    });
                }

                this._unitOfWork.Dispose();
                return BadRequest(new NormalResponse { 
                    Success = false,
                    Message = "Delete unsuccessfully",
                });
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
