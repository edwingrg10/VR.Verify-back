using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.Interfaces.UseCases;

[ApiController]
[Route("api/export")]
public class ExportController : ControllerBase
{
    private readonly IExcelUseCase _excelUseCase;

    public ExportController(IExcelUseCase excelUseCase)
    {
        _excelUseCase = excelUseCase;
    }

    [HttpGet("generate-template/{companyId}/{countryId}")]
    public async Task<IActionResult> GenerateExcelTemplate(int companyId, int countryId)
    {
        var stream = await _excelUseCase.GenerateExcelTemplateAsync(companyId, countryId);

        if (stream == null)
        {
            return NotFound(new
            {
                message = "No se encontraron hojas configuradas para los parámetros proporcionados",
                companyId,
                countryId
            });
        }

        return File(
            stream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Plantilla_{companyId}_{countryId}.xlsx"
        );
    }
}
