using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

public interface ICompanyRepository
{
    Task<ResponseDTO> GetCompaniesAsync();
    Task<ResponseDTO> GetCompanyByIdAsync(int id);
    Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto);
    Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto);
    Task<ResponseDTO> DeleteCompanyAsync(int id);
}
