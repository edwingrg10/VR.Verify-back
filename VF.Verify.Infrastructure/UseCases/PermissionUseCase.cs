using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;

public class PermissionUseCase : IPermissionUseCase
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionUseCase(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<ResponseDTO> GetAllPermissions()
    {
        return await _permissionRepository.GetAllPermissions();
    }

    public async Task<ResponseDTO> GetPermissionById(int id)
    {
        if (id <= 0)
            return new ResponseDTO { Message = "ID de permiso inválido" };

        return await _permissionRepository.GetPermissionById(id);
    }

    public async Task<ResponseDTO> CreatePermission(PermissionCreateDTO permissionDto)
    {
        if (string.IsNullOrWhiteSpace(permissionDto.Name))
            return new ResponseDTO { Message = "El nombre del permiso es requerido" };

        var permission = new Permission { Name = permissionDto.Name };
        return await _permissionRepository.CreatePermission(permission);
    }

    public async Task<ResponseDTO> UpdatePermission(PermissionUpdateDTO permissionDto)
    {
        if (permissionDto.Id <= 0)
            return new ResponseDTO { Message = "ID de permiso inválido" };

        if (string.IsNullOrWhiteSpace(permissionDto.Name))
            return new ResponseDTO { Message = "El nombre del permiso es requerido" };

        var permission = new Permission
        {
            Id = permissionDto.Id,
            Name = permissionDto.Name
        };

        return await _permissionRepository.UpdatePermission(permission);
    }

    public async Task<ResponseDTO> DeletePermission(int id)
    {
        if (id <= 0)
            return new ResponseDTO { Message = "ID de permiso inválido" };

        return await _permissionRepository.DeletePermission(id);
    }
}