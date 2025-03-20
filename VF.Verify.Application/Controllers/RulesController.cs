using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly IRuleUseCase _ruleUseCase;

        public RulesController(IRuleUseCase ruleUseCase)
        {
            _ruleUseCase = ruleUseCase;
        }

        [Authorize]
        [HttpPost("upload-rules/{companyCountryId}")]
        public async Task<IActionResult> UploadRules(int companyCountryId, IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "No se ha proporcionado un archivo Excel" });

            try
            {
                var response = await _ruleUseCase.ProcessRules(companyCountryId, excelFile);

                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO { IsSuccess = false, Message = $"Error interno: {ex.Message}" });
            }
        }
    }
}
