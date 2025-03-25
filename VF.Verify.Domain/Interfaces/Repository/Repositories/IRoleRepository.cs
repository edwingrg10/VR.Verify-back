using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories;

public interface IRoleRepository
{
    Task<ResponseDTO> GetAllRoles();
    Task<ResponseDTO> GetRoleById(int id);
    Task<ResponseDTO> CreateRole(Role role);
    Task<ResponseDTO> UpdateRole(Role role);
    Task<ResponseDTO> DeleteRole(int id);
    Task<ResponseDTO> AssignPermissions(int roleId, List<int> permissionIds);
    Task<ResponseDTO> GetRolePermissions(int roleId);

}
