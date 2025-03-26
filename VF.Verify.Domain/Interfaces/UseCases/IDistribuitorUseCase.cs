using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IDistribuitorUseCase
    {
        Task<ResponseDTO> GetDistribuitorsAsync();
        Task<ResponseDTO> GetDistribuitorByIdAsync(int id);
        Task<ResponseDTO> CreateDistribuitorAsync(Distributor distribuitor);
        Task<ResponseDTO> UpdateDistribuitorAsync(Distributor distribuitor);
        Task<ResponseDTO> DeleteDistribuitorAsync(int id);
    }

}
