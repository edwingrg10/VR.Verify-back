using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IPermissionRepository
    {
        Task<ResponseDTO> GetAllPermissions();
        Task<ResponseDTO> GetPermissionById(int id);
        Task<ResponseDTO> CreatePermission(Permission permission);
        Task<ResponseDTO> UpdatePermission(Permission permission);
        Task<ResponseDTO> DeletePermission(int id);
    }
}
