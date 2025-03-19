using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<ResponseDTO> GetCompaniesAsync();
        Task<ResponseDTO> GetCompanyByIdAsync(int id);
        Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto);
        Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto);
        Task<ResponseDTO> DeleteCompanyAsync(int id);
    }


}
