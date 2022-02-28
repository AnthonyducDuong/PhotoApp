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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Dangki([FromBody]UserModel userModel)
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
    }
}
