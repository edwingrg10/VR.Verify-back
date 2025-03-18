using Microsoft.Data.SqlClient;

namespace VF.Verify.Domain.Interfaces.Repository.DataContext
{
    public interface IDataContext : IAsyncDisposable
    {
        Task<SqlConnection> GetConnectionAsync(bool? connection);
        Task<SqlCommand> CreateCommandAsync(bool? connection);
    }
}
