using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Application.Interfaces
{
    public interface IRoleUseCase
    {
        Task<ResponseDTO> GetRolesAsync();
        Task<ResponseDTO> GetRoleByIdAsync(int id);
        Task<ResponseDTO> CreateRoleAsync(string roleName);
        Task<ResponseDTO> UpdateRoleAsync(Role roleDto);
        Task<ResponseDTO> DeleteRoleAsync(int id);
    }
}
