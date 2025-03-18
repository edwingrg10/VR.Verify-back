using VF.Verify.Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace VF.Verify.Domain.Interfaces.Services
{
    public interface IExcelParserService
    {
        Task<List<ExcelRuleData>> ParseExcel(IFormFile file);
    }
}