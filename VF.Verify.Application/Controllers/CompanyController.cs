using Azure;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var response = await _companyService.GetCompaniesAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var response = await _companyService.GetCompanyByIdAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDTO companyDto)
        {
            if (string.IsNullOrWhiteSpace(companyDto.Name) || string.IsNullOrWhiteSpace(companyDto.Nit))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "El NIT y el nombre de la compañía son obligatorios" });
            }

            var response = await _companyService.CreateCompanyAsync(companyDto);
            return CreatedAtAction(nameof(GetCompanyById), new { id = ((Company)response.Data).Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var response = await _companyService.DeleteCompanyAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPut]
        public async Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto)
        {
            var updatedCompany = await _companyService.UpdateCompanyAsync(companyDto);

            if (updatedCompany.Data == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Company no encontrada o el distribuidor no existe" };
            }

            return new ResponseDTO { IsSuccess = true, Message = "Company actualizada exitosamente", Data = companyDto };
        }
    }

}
