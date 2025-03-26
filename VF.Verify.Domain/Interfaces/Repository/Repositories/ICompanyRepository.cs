using VF.Verify.Domain.DTOs;

public interface ICompanyRepository
{
    Task<ResponseDTO> GetCompaniesAsync();
    Task<ResponseDTO> GetCompanyByIdAsync(int id);
    Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto);
    Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto);
    Task<ResponseDTO> DeleteCompanyAsync(int id);
}
