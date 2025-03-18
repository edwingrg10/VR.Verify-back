using Microsoft.Data.SqlClient;
using System.Reflection;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Services
{
    public class SqlCommandServices : ISqlCommandService

    {
        public void AddParameters<T>(SqlCommand command, T parameters)
        {
            if (parameters != null)
            {
                PropertyInfo[] properties = parameters.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    string parameterName = $"@{property.Name}";
                    object? value = property.GetValue(parameters);

                    command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
                }
            }
        }

    }
}
