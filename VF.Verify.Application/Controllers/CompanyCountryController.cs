using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    [Route("api/company-country")]
    public class CompanyCountryController : ControllerBase
    {
        private readonly ICompanyCountryUseCase _useCase;

        public CompanyCountryController(ICompanyCountryUseCase useCase)
        {
            _useCase = useCase;
        }

        [Authorize]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignCompanyToCountry([FromBody] AssignCompanyCountryDTO dto)
        {
            var response = await _useCase.AssignCompanyToCountry(dto.CompanyId, dto.CountryId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [Authorize]
        [HttpDelete("unassign")]
        public async Task<IActionResult> UnassignCompanyFromCountry([FromBody] AssignCompanyCountryDTO dto)
        {
            var response = await _useCase.UnassignCompanyFromCountry(dto.CompanyId, dto.CountryId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [Authorize]
        [HttpGet("companies/{countryId}")]
        public async Task<IActionResult> GetCompaniesByCountry(int countryId)
        {
            var response = await _useCase.ListCompaniesByCountry(countryId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("countries/{companyId}")]
        public async Task<IActionResult> GetCountriesByCompany(int companyId)
        {
            var response = await _useCase.ListCountriesByCompany(companyId);
            return Ok(response);
        }
    }

}
