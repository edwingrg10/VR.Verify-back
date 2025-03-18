using System.Diagnostics;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class UserRepository(IExecuteStoredProcedureService executeStoredProcedureService) : IUserRepository
    {
        private readonly IExecuteStoredProcedureService _executeStoredProcedureService = executeStoredProcedureService;

        public async Task<ResponseDTO> GetUsers()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await _executeStoredProcedureService.ExecuteDataStoredProcedure(false, "dbo.GetUser", null, MapToListHelper.MapToList<UserDto>);
            stopwatch.Stop();
            return result;
        }
    }
}
