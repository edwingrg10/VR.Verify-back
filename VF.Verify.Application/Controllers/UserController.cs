using Microsoft.AspNetCore.Mvc;
using VF.Verify.Application.Helpers;
using VF.Verify.Domain.Interfaces.Services;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    public class UserController(IUserUseCase userUseCase, ILogService logService) : ControllerBase
    {
        private readonly IUserUseCase _userUseCase = userUseCase;
        private readonly ILogService _logService = logService;

        [HttpGet("/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return await HandleResponses.HandleResponse(() => _userUseCase.GetUsers(), _logService, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await HandleResponses.HandleResponse(() => _userUseCase.GetUserById(id), _logService, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

    }
}
