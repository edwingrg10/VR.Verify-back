using Microsoft.AspNetCore.Http;
using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IRuleRepository
    {
        Task<ResponseDTO> ProcessRules(int companyCountryId, IFormFile excelFile);
    }
}
