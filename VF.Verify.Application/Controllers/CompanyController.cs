using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyUseCase _companyUseCase;

    public CompanyController(ICompanyUseCase companyUseCase)
    {
        _companyUseCase = companyUseCase;
    }

    [Authorize]
    [HttpGet]
    public async Task<ResponseDTO> GetCompanies() 
    {
        return await _companyUseCase.GetCompaniesAsync();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ResponseDTO> GetCompanyById(int id) 
    {
        return await _companyUseCase.GetCompanyByIdAsync(id);
    }

    [Authorize]
    [HttpPost]
    public async Task<ResponseDTO> CreateCompany([FromBody] CreateCompanyDTO companyDto)
    {
        return await _companyUseCase.CreateCompanyAsync(companyDto);
    }

    [Authorize]
    [HttpPut]
    public async Task<ResponseDTO> UpdateCompany([FromBody] UpdateCompanyDTO companyDto)
    {
        return await _companyUseCase.UpdateCompanyAsync(companyDto);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ResponseDTO> DeleteCompany(int id) 
    {
        return await _companyUseCase.DeleteCompanyAsync(id);
    }
}
