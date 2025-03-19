using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.UseCases;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Infrastructure.Repository.Repositories;

namespace VF.Verify.Infrastructure.UseCases
{
    public class CountryUseCase : ICountryUseCase
    {
        private readonly IContryRepository _contryRepository;

        public CountryUseCase(IContryRepository contryRepository)
        {
            _contryRepository = contryRepository;
        }
        public async Task<ResponseDTO> CreateCountryAsync(Country country)
        {
            return await _contryRepository.CreateCountryAsync(country);
        }

        public async Task<ResponseDTO> DeleteCountryAsync(int id)
        {
            return await _contryRepository.DeleteCountryAsync(id);
        }

        public async Task<ResponseDTO> GetCountriesAsync()
        {
            return await _contryRepository.GetCountriesAsync();
        }

        public async Task<ResponseDTO> GetCountryByIdAsync(int id)
        {
            return await _contryRepository.DeleteCountryAsync(id);
        }

        public async Task<ResponseDTO> UpdateCountryAsync(Country country)
        {
            return await _contryRepository.UpdateCountryAsync(country);
        }
    }
}
