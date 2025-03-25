using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

public class RoleUseCase : IRoleUseCase
{
    private readonly IRoleRepository _roleRepository;

    public RoleUseCase(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ResponseDTO> GetAllRoles()
    {
        return await _roleRepository.GetAllRoles();
    }
    public async Task<ResponseDTO> GetRoleById(int id)
    {
        if (id <= 0)
            return new ResponseDTO { Message = "ID de rol inválido" };

        return await _roleRepository.GetRoleById(id);
    }

    public async Task<ResponseDTO> CreateRole(RoleCreateDTO roleDto)
    {
        if (string.IsNullOrWhiteSpace(roleDto.Name))
            return new ResponseDTO { Message = "El nombre del rol es requerido" };

        var role = new Role { Name = roleDto.Name };
        return await _roleRepository.CreateRole(role);
    }

    public async Task<ResponseDTO> UpdateRole(RoleUpdateDTO roleDto)
    {
        if (string.IsNullOrWhiteSpace(roleDto.Name))
            return new ResponseDTO { Message = "El nombre del rol es requerido" };

        var role = new Role
        {
            Id = roleDto.Id,
            Name = roleDto.Name
        };

        return await _roleRepository.UpdateRole(role);
    }

    public async Task<ResponseDTO> DeleteRole(int id)
    {
        return await _roleRepository.DeleteRole(id);
    }

    public async Task<ResponseDTO> AssignPermissionsToRole(AssignPermissionsDTO request)
    {
        if (request.PermissionIds == null || !request.PermissionIds.Any())
            return new ResponseDTO { Message = "Se requieren permisos para asignar" };

        return await _roleRepository.AssignPermissions(request.RoleId, request.PermissionIds);
    }

    public async Task<ResponseDTO> GetRolePermissions(int roleId)
    {
        var response = await _roleRepository.GetRolePermissions(roleId);

        if (response.Data is RoleWithPermissionsDTO dto && !dto.Permissions.Any())
        {
            response.Message = "El rol no tiene permisos asignados";
        }

        return response;
    }
}