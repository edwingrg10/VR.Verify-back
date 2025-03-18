using Microsoft.AspNetCore.Http;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Infrastructure.UseCases
{
    public class RuleUseCase(IRuleRepository ruleRepository) : IRuleUseCase
    {
        private readonly IRuleRepository _ruleRepository = ruleRepository;

        public async Task<ResponseDTO> ProcessRules(int companyCountryId, IFormFile excelFile)
        {
            return await _ruleRepository.ProcessRules(companyCountryId, excelFile);
        }
    }

}
