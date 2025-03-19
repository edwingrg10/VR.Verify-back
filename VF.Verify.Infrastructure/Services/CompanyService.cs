using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Services;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyUseCase _companyUseCase;

        public CompanyService(ICompanyUseCase companyUseCase)
        {
            _companyUseCase = companyUseCase;
        }

        public async Task<ResponseDTO> GetCompaniesAsync()
        {
            var companies = await _companyUseCase.GetCompaniesAsync();
            return new ResponseDTO { IsSuccess = true, Message = "Empresas obtenidas correctamente", Data = companies };
        }

        public async Task<ResponseDTO> GetCompanyByIdAsync(int id)
        {
            var company = await _companyUseCase.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Empresa no encontrada" };
            }
            return new ResponseDTO { IsSuccess = true, Message = "Empresa obtenida correctamente", Data = company };
        }

        public async Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
        {
            var createdCompany = await _companyUseCase.CreateCompanyAsync(companyDto);
            return new ResponseDTO { IsSuccess = true, Message = "Empresa creada exitosamente", Data = createdCompany };
        }

        public async Task<ResponseDTO> DeleteCompanyAsync(int id)
        {
            var isDeleted = await _companyUseCase.DeleteCompanyAsync(id);
            if (!isDeleted)
            {
                return new ResponseDTO { IsSuccess = false, Message = "No se encontró la empresa para eliminar" };
            }
            return new ResponseDTO { IsSuccess = true, Message = "Empresa eliminada exitosamente" };
        }

        public async Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto)
        {
            var updatedCompany = await _companyUseCase.UpdateCompanyAsync(companyDto);

            if (updatedCompany == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Company not found or Distributor does not exist" };
            }

            return new ResponseDTO { IsSuccess = true, Message = "Company updated successfully", Data = companyDto };
        }


    }


}