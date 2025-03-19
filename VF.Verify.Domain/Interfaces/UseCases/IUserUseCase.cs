using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IUserUseCase
    {
        Task<ResponseDTO> GetUsers();

        Task<ResponseDTO> GetUserById(int userId);
    }
}
