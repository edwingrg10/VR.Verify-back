using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyUseCase _companyUseCase;

    public CompanyController(ICompanyUseCase companyUseCase)
    {
        _companyUseCase = companyUseCase;
    }

    [HttpGet]
    public Task<ResponseDTO> GetCompanies() => _companyUseCase.GetCompaniesAsync();

    [HttpGet("{id}")]
    public async Task<ResponseDTO> GetCompanyById(int id) 
    {
        return await _companyUseCase.GetCompanyByIdAsync(id);
    }

    [HttpPost]
    public async Task<ResponseDTO> CreateCompany([FromBody] CreateCompanyDTO companyDto)
    {
        return await _companyUseCase.CreateCompanyAsync(companyDto);
    }

    [HttpPut]
    public async Task<ResponseDTO> UpdateCompany([FromBody] UpdateCompanyDTO companyDto)
    {
        return await _companyUseCase.UpdateCompanyAsync(companyDto);
    }


    [HttpDelete("{id}")]
    public Task<ResponseDTO> DeleteCompany(int id) => _companyUseCase.DeleteCompanyAsync(id);
}
