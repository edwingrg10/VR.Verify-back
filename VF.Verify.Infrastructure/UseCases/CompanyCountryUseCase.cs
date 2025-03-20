using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Infrastructure.UseCases
{
    public class CompanyCountryUseCase : ICompanyCountryUseCase
    {
        private readonly ICompanyCountryRepository _repository;

        public CompanyCountryUseCase(ICompanyCountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO> AssignCompanyToCountry(int companyId, int countryId)
        {
            return await _repository.AddCompanyToCountryAsync(companyId, countryId);
        }

        public async Task<ResponseDTO> UnassignCompanyFromCountry(int companyId, int countryId)
        {
            return await _repository.RemoveCompanyFromCountryAsync(companyId, countryId);
        }

        public async Task<ResponseDTO> ListCompaniesByCountry(int countryId)
        {
            return await _repository.GetCompaniesByCountryAsync(countryId);
        }

        public async Task<ResponseDTO> ListCountriesByCompany(int companyId)
        {
            return await _repository.GetCountriesByCompanyAsync(companyId);
        }
    }

}
