using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;
using VF.Verify.Infrastructure.Repository.Repositories;

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

        public async Task<List<CompanyDto>> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetCompaniesAsync();

            return companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Nit = c.Nit,
                Name = c.Name,
                ContactEmail = c.ContactEmail,
                ContactFullName = c.ContactFullName,
                DistributorId = c.DistributorId,
                IsDistributor = c.IsDistributor,
                IsActive = c.IsActive,
                Distributor = new DistributorDto
                {
                    Id = c.Distributor.Id,
                    Name = c.Distributor.Name,
                    Logo = c.Distributor.Logo
                }
            }).ToList();
        }


        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return null;
            }

            return new CompanyDto
            {
                Id = company.Id,
                Nit = company.Nit,
                Name = company.Name,
                ContactEmail = company.ContactEmail,
                ContactFullName = company.ContactFullName,
                IsDistributor = company.IsDistributor,
                IsActive = company.IsActive,
                Distributor = company.Distributor != null
            ? new DistributorDto
            {
                Id = company.Distributor.Id,
                Name = company.Distributor.Name
            }
            : null
            };
        }

        public async Task<Company> CreateCompanyAsync(CreateCompanyDTO companyDto)
        {
            var newCompany = new Company
            {
                Nit = companyDto.Nit,
                Name = companyDto.Name,
                ContactEmail = companyDto.ContactEmail,
                ContactFullName = companyDto.ContactFullName,
                DistributorId = companyDto.DistributorId,
                IsDistributor = companyDto.IsDistributor,
                IsActive = companyDto.IsActive
            };

            return await _companyRepository.CreateCompanyAsync(newCompany);
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            return await _companyRepository.DeleteCompanyAsync(id);
        }

        public async Task<Company> UpdateCompanyAsync(UpdateCompanyDTO companyDto)
        {
            var existingCompany = await _companyRepository.GetCompanyByIdAsync(companyDto.Id);

            if (existingCompany == null)
            {
                return null;
            }

            var distributorExists = await __distributorRepository.ExistsAsync(companyDto.DistributorId);
            if (!distributorExists)
            {
                return null;
            }

            existingCompany.Nit = companyDto.Nit;
            existingCompany.Name = companyDto.Name;
            existingCompany.ContactEmail = companyDto.ContactEmail;
            existingCompany.ContactFullName = companyDto.ContactFullName;
            existingCompany.IsDistributor = companyDto.IsDistributor;
            existingCompany.IsActive = companyDto.IsActive;
            existingCompany.DistributorId = companyDto.DistributorId; // Asignamos solo el ID

            return await _companyRepository.UpdateCompanyAsync(existingCompany);

        }



    }

}
