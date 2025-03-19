using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface ICountryUseCase
    {
        Task<ResponseDTO> GetCountriesAsync();
        Task<ResponseDTO> GetCountryByIdAsync(int id);
        Task<ResponseDTO> CreateCountryAsync(Country country);
        Task<ResponseDTO> UpdateCountryAsync(Country country);
        Task<ResponseDTO> DeleteCountryAsync(int id);
    }
}
