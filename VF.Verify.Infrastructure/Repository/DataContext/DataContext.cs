using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using VF.Verify.Domain.Interfaces.Repository.DataContext;
using VF.Verify.Domain.Interfaces.Services;

public class DataContext(IConfiguration configuration, ILogService logService, IHttpContextAccessor httpContextAccessor) : IDataContext, IAsyncDisposable
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private readonly ILogService _logService = logService ?? throw new ArgumentNullException(nameof(logService));
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    private SqlConnection? _connection;

    public async Task<SqlCommand> CreateCommandAsync(bool? isconnected)
    {
        try
        {
            var connection = await GetConnectionAsync(isconnected);
            return new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text
            };
        }
        catch (Exception ex)
        {
            _logService.SaveLogsMessages($"Error creating command: {ex.Message}");
            throw;
        }
    }

    public async Task<SqlConnection> GetConnectionAsync(bool? connection)
    {
        try
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (connection == true)
                {
                    _connection = new SqlConnection(connectionString);
                    await _connection.OpenAsync();
                    return _connection;
                }
                _connection = new SqlConnection(connectionString);
                await _connection.OpenAsync();
            }
            return _connection;
        }
        catch (SqlException sqlEx)
        {
            _logService.SaveLogsMessages($"SQL Error: {sqlEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logService.SaveLogsMessages($"Unexpected Error: {ex.Message}");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null && _connection.State != ConnectionState.Closed)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
}
