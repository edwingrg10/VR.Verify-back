using Microsoft.Data.SqlClient;
using System.Data;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.DataContext;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Services
{
    public class ExecuteStoredProcedureService(IDataContext context, ILogService logService, ISqlCommandService sqlCommandService) : IExecuteStoredProcedureService

    {
        private readonly IDataContext _context = context;
        private readonly ILogService _logService = logService;
        private readonly ISqlCommandService _sqlCommandService = sqlCommandService;

        private void HandleException(Exception ex, string storedProcedureName, ResponseDTO response)
        {
            _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
            response.Message += ex.ToString();
        }

        public async Task<ResponseDTO> ExecuteStoredProcedure(bool? connection, string storedProcedureName, object parameters)
        {
            ResponseDTO response = new ResponseDTO();
            response.IsSuccess = false;
            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();
                response.Message = "Operación Exitosa";
                response.IsSuccess = true;
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }


        public async Task<ResponseDTO> ExecuteDataStoredProcedureV2<TResult>(bool? connection, string storedProcedureName, object parameters, Func<SqlDataReader, Task<List<TResult>>> mapFunction, int resultSetIndex = 0)
        {
            ResponseDTO response = new ResponseDTO
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                List<TResult> resultList = null;
                int currentSetIndex = 0;

                while (await reader.ReadAsync())
                {
                    if (currentSetIndex == resultSetIndex)
                    {
                        resultList = await mapFunction(reader);
                        break;
                    }

                    if (!await reader.NextResultAsync())
                    {
                        throw new InvalidOperationException("El índice de conjunto de resultados solicitado no existe.");
                    }

                    currentSetIndex++;
                }

                response.Data = resultList;
                response.Message = "Operación Exitosa";
                response.IsSuccess = true;
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDTO> ExecuteDataStoredProcedureSecondPosition<TResult>(bool? connection, string storedProcedureName, object parameters, Func<SqlDataReader, List<TResult>> mapFunction)
        {
            ResponseDTO response = new()
            {
                IsSuccess = false
            };
            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                bool hasErrorColumn = false;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals("error", StringComparison.OrdinalIgnoreCase))
                    {
                        hasErrorColumn = true;
                        break;
                    }
                }

                if (hasErrorColumn)
                {
                    if (await reader.ReadAsync())
                    {
                        string error = reader["error"]?.ToString();
                        if (!string.IsNullOrEmpty(error))
                        {
                            response.Message = error;
                            return response;
                        }
                    }

                    await reader.NextResultAsync();
                    List<TResult> resultList = mapFunction(reader);
                    if (resultList.Count > 0)
                    {
                        response.Data = resultList[0];
                        response.Message = "Operación Exitosa";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "No se encontraron datos en la segunda tabla";
                    }
                }
                else
                {
                    List<TResult> resultList = mapFunction(reader);
                    if (resultList.Count > 0)
                    {
                        response.Data = resultList[0];
                        response.Message = "Operación Exitosa";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "No se encontraron datos";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }



        public async Task<ResponseDTO> ExecuteDataStoredProcedure<TResult>(bool? connection, string storedProcedureName, object parameters, Func<SqlDataReader, List<TResult>> mapFunction)
        {
            ResponseDTO response = new()
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                List<TResult> resultList = mapFunction(reader);

                response.Data = resultList;
                response.Message = "Operación Exitosa";
                response.IsSuccess = true;
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDTO> ExecuteSingleObjectStoredProcedure<TResult>(
            bool? connection,
            string storedProcedureName,
            object parameters,
            Func<SqlDataReader, Task<TResult>> mapFunction)
        {
            ResponseDTO response = new ResponseDTO
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection).ConfigureAwait(false);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);

                using SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                TResult result = default(TResult);

                if (await reader.ReadAsync().ConfigureAwait(false))
                {
                    result = await mapFunction(reader).ConfigureAwait(false);
                }

                response.Data = result;
                response.Message = "Operación Exitosa";
                response.IsSuccess = true;
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDTO> ExecuteJSONStoredProcedure(bool? connection, string storedProcedureName, object parameters)
        {
            ResponseDTO response = new()
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                _sqlCommandService.AddParameters(command, parameters);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                bool hasErrorMessageColumn = reader.FieldCount > 0 && Enumerable.Range(0, reader.FieldCount).Any(i => reader.GetName(i) == "ErrorMessage");

                if (await reader.ReadAsync() && hasErrorMessageColumn)
                {
                    var errorMessage = reader["ErrorMessage"].ToString();
                    if (errorMessage == "No Access")
                    {
                        response.Message = "No tienes permisos para realizar esta operación.";
                        return response;
                    }
                }

                if (reader.HasRows && await reader.ReadAsync())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("JSONString")))
                    {
                        var dataResponse = reader.GetString(reader.GetOrdinal("JSONString"));
                        response.Data = dataResponse ?? null;
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "No se encontraron datos.";
                    }
                }
                else
                {
                    response.Message = "No se encontraron datos.";
                }
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar el SP {storedProcedureName}: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<ResponseDTO> ExecuteTableStoredProcedure<TResult>(bool? connection, string storedProcedureName, object? parameters, Func<SqlDataReader, List<TResult>> mapFunction)
        {
            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            try
            {
                using var command = await _context.CreateCommandAsync(connection);
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    _sqlCommandService.AddParameters(command, parameters);
                }

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync() && reader.FieldCount == 1 && reader.GetValue(0).ToString() == "No Access")
                {
                    response.Message = "No tienes permisos para realizar esta operación.";
                    return response;
                }

                if (reader.HasRows)
                {
                    var resultList = mapFunction(reader);
                    int totalRecords = 0;

                    if (await reader.NextResultAsync() && await reader.ReadAsync())
                    {
                        totalRecords = reader.GetInt32(reader.GetOrdinal("TotalRecords"));
                    }

                    response.Data = new { Results = resultList, TotalRecords = totalRecords };
                    response.Message = "Operación Exitosa";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "No se encontraron resultados.";
                }
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, storedProcedureName, response);
            }
            catch (Exception ex)
            {
                HandleException(ex, storedProcedureName, response);
            }

            return response;
        }

        private void HandleSqlException(SqlException sqlEx, string storedProcedureName, ResponseDTO response)
        {
            _logService.SaveLogsMessages($"Error SQL al ejecutar el SP {storedProcedureName}: {sqlEx.Message}");
            response.Message = sqlEx.Message;
        }

        public async Task<ResponseDTO> ExecuteQuery<TResult>(bool? connection, string query, Func<SqlDataReader, List<TResult>> mapFunction)
        {
            ResponseDTO response = new ResponseDTO
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                List<TResult> resultList = mapFunction(reader);

                response.Data = resultList;
                response.Message = "Operación Exitosa";
                response.IsSuccess = true;
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar la consulta: {query}, Error: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar la consulta: {query}, Error: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<ResponseDTO> ExecuteSqlFunction<TResult>(bool? connection, string query, Func<SqlDataReader, TResult> mapFunction = null)
        {
            ResponseDTO response = new ResponseDTO
            {
                IsSuccess = false
            };

            try
            {
                using SqlCommand command = await _context.CreateCommandAsync(connection);
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                if (mapFunction == null)
                {
                    var scalarResult = await command.ExecuteScalarAsync();
                    response.Data = scalarResult;
                    response.Message = "Operación Exitosa";
                    response.IsSuccess = true;
                }
                else
                {
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    TResult result = default;

                    if (await reader.ReadAsync())
                    {
                        result = mapFunction(reader);
                    }

                    response.Data = result;
                    response.Message = "Operación Exitosa";
                    response.IsSuccess = true;
                }
            }
            catch (SqlException sqlEx)
            {
                _logService.SaveLogsMessages($"Error SQL al ejecutar la consulta: {query}, Error: {sqlEx.Message}");
                response.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                _logService.SaveLogsMessages($"Se ha producido un error al ejecutar la consulta: {query}, Error: {ex.Message}");
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

    }
}
