using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDTO> GetRolesAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "Roles obtenidos exitosamente",
            Data = roles
        };
    }

    public async Task<ResponseDTO> GetRoleByIdAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return new ResponseDTO { IsSuccess = false, Message = "Rol no encontrado" };
        }

        return new ResponseDTO { IsSuccess = true, Message = "Rol obtenido exitosamente", Data = role };
    }

    public async Task<ResponseDTO> CreateRoleAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "Rol creado exitosamente",
            Data = role
        };
    }

    public async Task<ResponseDTO> UpdateRoleAsync(Role role)
    {
        var existingRole = await _context.Roles.FindAsync(role.Id);
        if (existingRole == null)
        {
            return new ResponseDTO { IsSuccess = false, Message = "Rol no encontrado" };
        }

        existingRole.Name = role.Name;
        await _context.SaveChangesAsync();

        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "Rol actualizado exitosamente",
            Data = existingRole
        };
    }

    public async Task<ResponseDTO> DeleteRoleAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return new ResponseDTO { IsSuccess = false, Message = "Rol no encontrado" };
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "Rol eliminado exitosamente",
            Data = role
        };
    }
}
