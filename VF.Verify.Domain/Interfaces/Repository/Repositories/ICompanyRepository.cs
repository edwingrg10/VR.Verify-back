using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company companyDto);
        Task<bool> DeleteCompanyAsync(int id);
    }
}
