using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Infrastructure.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<ResponseDTO> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<ResponseDTO> CreateUserAsync(UserDTO userDto)
        {
            return await _userRepository.CreateUserAsync(userDto);
        }

        public async Task<ResponseDTO> UpdateUserAsync(UserDTO userDto)
        {
            return await _userRepository.UpdateUserAsync(userDto);
        }

        public async Task<ResponseDTO> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<ResponseDTO> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<ResponseDTO> AuthenticateUserAsync(string email, string password)
        {
            return await _userRepository.AuthenticateUserAsync(email, password);
        }
    }
}
