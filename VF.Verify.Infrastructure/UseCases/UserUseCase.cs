using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.Services;
using VF.Verify.Domain.Interfaces.UseCases;
using VF.Verify.Infrastructure.Helpers;

namespace VF.Verify.Infrastructure.UseCases
{
    public class UserUseCase(IUserRepository userRepository, ILogService logService) : IUserUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogService _logService = logService;

        public async Task<ResponseDTO> GetUserById(int userId)
        {
            try
            {
                return await _userRepository.GetUserById(userId);
            }
            catch (Exception ex)
            {
                return ExceptionHelper.HandleException(_logService, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public async Task<ResponseDTO> GetUsers()
        {
            try
            {
                return await _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                return ExceptionHelper.HandleException(_logService, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }
    }
}
