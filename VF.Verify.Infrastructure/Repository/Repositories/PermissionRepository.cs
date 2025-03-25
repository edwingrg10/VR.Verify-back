using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDTO> GetAllPermissions()
    {
        try
        {
            var permissions = await _context.Permissions.ToListAsync();
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permisos obtenidos exitosamente",
                Data = permissions
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO
            {
                Message = $"Error al obtener permisos: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> GetPermissionById(int id)
    {
        try
        {
            var permission = await _context.Permissions.FindAsync(id);

            return permission == null
                ? new ResponseDTO { Message = "Permiso no encontrado" }
                : new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "Permiso obtenido exitosamente",
                    Data = permission
                };
        }
        catch (Exception ex)
        {
            return new ResponseDTO
            {
                Message = $"Error al obtener permiso: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> CreatePermission(Permission permission)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if (await _context.Permissions.AnyAsync(p => p.Name == permission.Name))
                return new ResponseDTO { Message = "El permiso ya existe" };

            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permiso creado exitosamente",
                Data = permission
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al crear permiso: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> UpdatePermission(Permission permission)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingPermission = await _context.Permissions.FindAsync(permission.Id);
            if (existingPermission == null)
                return new ResponseDTO { Message = "Permiso no encontrado" };

            if (await _context.Permissions.AnyAsync(p => p.Name == permission.Name && p.Id != permission.Id))
                return new ResponseDTO { Message = "El nombre del permiso ya existe" };

            existingPermission.Name = permission.Name;
            _context.Permissions.Update(existingPermission);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permiso actualizado exitosamente",
                Data = existingPermission
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al actualizar permiso: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> DeletePermission(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
                return new ResponseDTO { Message = "Permiso no encontrado" };

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permiso eliminado exitosamente"
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al eliminar permiso: {ex.Message}"
            };
        }
    }
}