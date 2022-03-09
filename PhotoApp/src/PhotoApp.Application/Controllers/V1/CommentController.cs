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
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

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
    }
}
