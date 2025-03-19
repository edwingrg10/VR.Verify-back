using System.Data;
using System.Diagnostics;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.Services;
using VR.Verify.Infrastructure.Helpers;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class UserRepository(IExecuteStoredProcedureService executeStoredProcedureService) : IUserRepository
    {
        private readonly IExecuteStoredProcedureService _executeStoredProcedureService = executeStoredProcedureService;

        public Task<ResponseDTO> CreateOrUpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetUserById(int userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var obj = new
            {
                UserId = userId
            };

            var result = await _executeStoredProcedureService.ExecuteDataStoredProcedure(false, "dbo.GetUser", obj, MapToListHelper.MapToList<UserDto>);
            stopwatch.Stop();
            return result;
        }

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
