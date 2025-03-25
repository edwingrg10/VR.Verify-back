using VF.Verify.Domain.DTOs;

public interface IRoleUseCase
{
    // Obtener todos los roles
    Task<ResponseDTO> GetAllRoles();

    // Obtener un rol por ID
    Task<ResponseDTO> GetRoleById(int id);

    // Crear nuevo rol
    Task<ResponseDTO> CreateRole(RoleCreateDTO role);

    // Actualizar rol existente
    Task<ResponseDTO> UpdateRole(RoleUpdateDTO role);

    // Eliminar rol
    Task<ResponseDTO> DeleteRole(int id);

    // Asignar permisos a un rol
    Task<ResponseDTO> AssignPermissionsToRole(AssignPermissionsDTO request);

    // (Opcional) Obtener permisos de un rol
    Task<ResponseDTO> GetRolePermissions(int roleId);
}