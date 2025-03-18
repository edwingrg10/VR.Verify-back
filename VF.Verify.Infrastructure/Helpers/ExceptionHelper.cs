using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Helpers
{
    public static class ExceptionHelper
    {
        public static ResponseDTO HandleException(ILogService logService, string methodName, Exception ex)
        {
            logService.SaveLogsMessages($"Se ha producido un error al ejecutar BLL: {methodName}: {ex.Message}");
            var response = new ResponseDTO
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
            return response;
        }
    }

}
