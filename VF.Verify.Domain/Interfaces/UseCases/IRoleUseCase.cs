using VF.Verify.Domain.DTOs;

public interface IRoleUseCase
{
    Task<ResponseDTO> GetAllRoles();
    Task<ResponseDTO> GetRoleById(int id);
    Task<ResponseDTO> CreateRole(RoleCreateDTO role);
    Task<ResponseDTO> UpdateRole(RoleUpdateDTO role);
    Task<ResponseDTO> DeleteRole(int id);
    Task<ResponseDTO> AssignPermissionsToRole(AssignPermissionsDTO request);
    Task<ResponseDTO> GetRolePermissions(int roleId);
}