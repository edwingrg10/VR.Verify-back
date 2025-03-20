using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface ICompanyCountryRepository
    {
        Task<ResponseDTO> AddCompanyToCountryAsync(int companyId, int countryId);
        Task<ResponseDTO> RemoveCompanyFromCountryAsync(int companyId, int countryId);
        Task<ResponseDTO> GetCompaniesByCountryAsync(int countryId);
        Task<ResponseDTO> GetCountriesByCompanyAsync(int companyId);
    }

}
