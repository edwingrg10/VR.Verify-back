using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IUserUseCase
    {
        Task<ResponseDTO> GetUsersAsync();
        Task<ResponseDTO> GetUserByIdAsync(int id);
        Task<ResponseDTO> CreateUserAsync(UserDTO user);
        Task<ResponseDTO> UpdateUserAsync(UserDTO user);
        Task<ResponseDTO> DeleteUserAsync(int id);
        Task<ResponseDTO> GetUserByEmailAsync(string email);
        Task<ResponseDTO> AuthenticateUserAsync(string email, string password);
    }

}
