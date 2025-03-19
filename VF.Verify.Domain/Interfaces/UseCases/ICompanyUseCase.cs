using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface ICompanyUseCase
    {
        Task<List<CompanyDto>> GetCompaniesAsync();
        Task<CompanyDto> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(CreateCompanyDTO companyDto);
        Task<Company> UpdateCompanyAsync(UpdateCompanyDTO companyDto);
        Task<bool> DeleteCompanyAsync(int id);
    }

}
