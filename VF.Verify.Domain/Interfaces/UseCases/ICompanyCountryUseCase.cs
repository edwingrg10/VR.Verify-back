using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface ICompanyCountryUseCase
    {
        Task<ResponseDTO> AssignCompanyToCountry(int companyId, int countryId);
        Task<ResponseDTO> UnassignCompanyFromCountry(int companyId, int countryId);
        Task<ResponseDTO> ListCompaniesByCountry(int countryId);
        Task<ResponseDTO> ListCountriesByCompany(int companyId);
    }

}
