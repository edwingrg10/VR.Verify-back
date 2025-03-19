using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IContryRepository
    {
        Task<ResponseDTO> GetCountriesAsync();
        Task<ResponseDTO> GetCountryByIdAsync(int id);
        Task<ResponseDTO> CreateCountryAsync(Country country);
        Task<ResponseDTO> UpdateCountryAsync(Country country);
        Task<ResponseDTO> DeleteCountryAsync(int id);
    }
}
