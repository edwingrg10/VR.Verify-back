﻿using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryUseCase _countryUseCase;

        public CountryController(ICountryUseCase countryUseCase)
        {
            _countryUseCase = countryUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var response = await _countryUseCase.GetCountriesAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var response = await _countryUseCase.GetCountryByIdAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            var response = await _countryUseCase.CreateCountryAsync(country);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] Country country)
        {
            var response = await _countryUseCase.UpdateCountryAsync(country);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var response = await _countryUseCase.DeleteCountryAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
    }
}
