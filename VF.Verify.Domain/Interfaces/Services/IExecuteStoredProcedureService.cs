using Microsoft.Data.SqlClient;
using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.Services
{
    public interface IExecuteStoredProcedureService
    {
        Task<ResponseDTO> ExecuteStoredProcedure(bool? connection, string storedProcedureName, object parameters);
        Task<ResponseDTO> ExecuteJSONStoredProcedure(bool? connection, string storedProcedureName, object parameters);
        Task<ResponseDTO> ExecuteDataStoredProcedure<TResult>(bool? connection, string storedProcedureName, object? parameters, Func<SqlDataReader, List<TResult>> mapFunction);
        Task<ResponseDTO> ExecuteDataStoredProcedureV2<TResult>(bool? connection, string storedProcedureName, object parameters, Func<SqlDataReader, Task<List<TResult>>> mapFunction, int resultSetIndex = 0);
        Task<ResponseDTO> ExecuteQuery<TResult>(bool? connection, string query, Func<SqlDataReader, List<TResult>> mapFunction);
        Task<ResponseDTO> ExecuteTableStoredProcedure<TResult>(bool? connection, string storedProcedureName, object? parameters, Func<SqlDataReader, List<TResult>> mapFunction);
        Task<ResponseDTO> ExecuteSingleObjectStoredProcedure<TResult>(bool? connection, string storedProcedureName, object? parameters, Func<SqlDataReader, Task<TResult>> mapFunction);
        Task<ResponseDTO> ExecuteSqlFunction<TResult>(bool? connection, string query, Func<SqlDataReader, TResult> mapFunction = null);

    }

}
