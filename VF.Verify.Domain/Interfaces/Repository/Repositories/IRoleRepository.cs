using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories;

public interface IRoleRepository
{
    Task<ResponseDTO> GetRolesAsync();
    Task<ResponseDTO> GetRoleByIdAsync(int id);
    Task<ResponseDTO> CreateRoleAsync(Role role);
    Task<ResponseDTO> UpdateRoleAsync(Role role);
    Task<ResponseDTO> DeleteRoleAsync(int id);
}
