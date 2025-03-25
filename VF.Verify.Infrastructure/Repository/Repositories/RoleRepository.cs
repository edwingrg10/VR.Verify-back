using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDTO> GetAllRoles()
    {
        try
        {
            var roles = await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Permissions = r.RolePermissions.Select(rp => new RolePermissionDTO
                    {
                        PermissionId = rp.PermissionId,
                        PermissionName = rp.Permission.Name
                    }).ToList()
                })
                .ToListAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Roles obtenidos exitosamente",
                Data = roles
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO
            {
                Message = $"Error al obtener roles: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> GetRoleById(int id)
    {
        try
        {
            var role = await _context.Roles
                .Where(r => r.Id == id)
                .Select(r => new RoleWithPermissionsDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Permissions = r.RolePermissions
                        .Select(rp => new PermissionDTO
                        {
                            Id = rp.Permission.Id,
                            Name = rp.Permission.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return role != null
                ? new ResponseDTO { IsSuccess = true, Data = role }
                : new ResponseDTO { Message = "Rol no encontrado" };
        }
        catch (Exception ex)
        {
            return new ResponseDTO { Message = $"Error: {ex.Message}" };
        }
    }

    public async Task<ResponseDTO> CreateRole(Role role)
    {
        try
        {
            if (await _context.Roles.AnyAsync(r => r.Name == role.Name))
                return new ResponseDTO { Message = "El nombre del rol ya existe" };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Rol creado exitosamente",
                Data = role
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO
            {
                Message = $"Error al crear rol: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> UpdateRole(Role role)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
                return new ResponseDTO { Message = "Rol no encontrado" };

            if (await _context.Roles.AnyAsync(r => r.Name == role.Name && r.Id != role.Id))
                return new ResponseDTO { Message = "El nombre del rol ya existe" };

            existingRole.Name = role.Name;
            _context.Roles.Update(existingRole);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Rol actualizado exitosamente",
                Data = existingRole
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al actualizar rol: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> DeleteRole(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return new ResponseDTO { Message = "Rol no encontrado" };

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Rol eliminado exitosamente"
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al eliminar rol: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> AssignPermissions(int roleId, List<int> permissionIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                return new ResponseDTO { Message = "Rol no encontrado" };

            _context.RolePermissions.RemoveRange(role.RolePermissions);

            var permissions = await _context.Permissions
                .Where(p => permissionIds.Contains(p.Id))
                .ToListAsync();

            var newRolePermissions = permissions.Select(p => new RolePermission
            {
                RoleId = roleId,
                PermissionId = p.Id,
                Permission = p
            }).ToList();

            role.RolePermissions = newRolePermissions;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            await _context.Entry(role)
                .Collection(r => r.RolePermissions)
                .Query()
                .Include(rp => rp.Permission)
                .LoadAsync();

            var result = new RoleWithPermissionsDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = role.RolePermissions
                    .Select(rp => new PermissionDTO
                    {
                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name
                    })
                    .ToList()
            };

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permisos asignados exitosamente",
                Data = result
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDTO
            {
                Message = $"Error al asignar permisos: {ex.Message}"
            };
        }
    }

    public async Task<ResponseDTO> GetRolePermissions(int roleId)
    {
        try
        {
            var role = await _context.Roles
                .Where(r => r.Id == roleId)
                .Select(r => new RoleWithPermissionsDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Permissions = r.RolePermissions
                        .Select(rp => new PermissionDTO
                        {
                            Id = rp.Permission.Id,
                            Name = rp.Permission.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (role == null)
                return new ResponseDTO { Message = "Rol no encontrado" };

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Permisos obtenidos exitosamente",
                Data = role
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
}