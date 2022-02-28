using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Domain.Constants;
using PhotoApp.Domain.Interfaces.IServices;
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
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserService userService, IUnitOfWork unitOfWork)
        {
            this._userService = userService;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Dangki(UserModel userModel)
        {
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
