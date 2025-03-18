using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IUserRepository
    {
        Task<ResponseDTO> GetUsers();

    }
}
