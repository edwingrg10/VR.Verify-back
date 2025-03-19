using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IUserRepository
    {
        Task<ResponseDTO> GetUsers();

        Task<ResponseDTO> GetUserById(int userId);

        Task<ResponseDTO> CreateOrUpdateUser(UserDto user);

    }
}
