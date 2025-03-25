using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IPermissionUseCase
    {
        Task<ResponseDTO> GetAllPermissions();
        Task<ResponseDTO> GetPermissionById(int id);
        Task<ResponseDTO> CreatePermission(PermissionCreateDTO permission);
        Task<ResponseDTO> UpdatePermission(PermissionUpdateDTO permission);
        Task<ResponseDTO> DeletePermission(int id);
    }
}
