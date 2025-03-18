using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Application.Helpers
{
    public static class HandleResponses

    {
        public static async Task<ActionResult> HandleResponse(Func<Task<ResponseDTO>> action, ILogService _logService, string controllerName)
        {
            try
            {
                ResponseDTO response = await action.Invoke();

                if (response.IsSuccess && response.Data is FileContentResult fileResult)
                {
                    return fileResult;
                }
                if (response.IsSuccess && response.Data is FileStreamResult fileStreamResult)
                {
                    return fileStreamResult;
                }

                return new OkObjectResult(response);
            }
            catch (ValidationException ex)
            {
                _logService.SaveLogsMessages($"Error desde :: {controllerName} :: {ex.Message}");
                return new BadRequestObjectResult(new ResponseDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Error desde :: {controllerName} :: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

    }
}
