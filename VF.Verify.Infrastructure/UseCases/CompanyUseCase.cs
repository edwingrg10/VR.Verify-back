using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.UseCases
{
    public class CompanyUseCase : ICompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDistribuitorRepository __distributorRepository;

        public CompanyUseCase(ICompanyRepository companyRepository, IDistribuitorRepository distributorRepository)
        {
            _companyRepository = companyRepository;
            __distributorRepository = distributorRepository;
        }

        public Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
        {
            return _companyRepository.CreateCompanyAsync(companyDto);
        }

        public Task<ResponseDTO> DeleteCompanyAsync(int id)
        {
            return _companyRepository.DeleteCompanyAsync(id);
        }

        public Task<ResponseDTO> GetCompaniesAsync()
        {
            return _companyRepository.GetCompaniesAsync();
        }

        public Task<ResponseDTO> GetCompanyByIdAsync(int id)
        {
            return _companyRepository.GetCompanyByIdAsync(id);
        }

        public Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto)
        {
            return _companyRepository.UpdateCompanyAsync(companyDto);
        }
    }

}
