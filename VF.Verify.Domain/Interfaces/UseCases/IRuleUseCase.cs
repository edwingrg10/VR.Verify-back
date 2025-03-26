using Microsoft.AspNetCore.Http;
using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IRuleUseCase
    {
        Task<ResponseDTO> ProcessRules(int companyCountryId, IFormFile excelFile);
    }

}
