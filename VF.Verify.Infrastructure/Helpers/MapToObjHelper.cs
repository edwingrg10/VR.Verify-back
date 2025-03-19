using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text.Json.Serialization;

namespace VR.Verify.Infrastructure.Helpers
{
    public static class MapToObjHelper
    {
        public static async Task<T> MapToObj<T>(SqlDataReader dataReader) where T : new()
        {
            var obj = new T();
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (await dataReader.ReadAsync().ConfigureAwait(false))
            {
                foreach (var prop in properties)
                {
                    var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;

                    if (FieldExists(dataReader, jsonPropertyName) && !object.Equals(dataReader[jsonPropertyName], DBNull.Value))
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        var safeValue = Convert.ChangeType(await GetFieldValueAsync(dataReader, jsonPropertyName, type).ConfigureAwait(false), type);

                        prop.SetValue(obj, safeValue, null);
                    }
                }
            }
            return obj;
        }

        private static async Task<object> GetFieldValueAsync(SqlDataReader dataReader, string fieldName, Type targetType)
        {
            int ordinal = dataReader.GetOrdinal(fieldName);

            if (targetType == typeof(string))
                return await dataReader.GetFieldValueAsync<string>(ordinal).ConfigureAwait(false);
            else if (targetType == typeof(int))
                return await dataReader.GetFieldValueAsync<int>(ordinal).ConfigureAwait(false);
            else if (targetType == typeof(DateTime))
                return await dataReader.GetFieldValueAsync<DateTime>(ordinal).ConfigureAwait(false);
            else
                return await dataReader.GetFieldValueAsync<object>(ordinal).ConfigureAwait(false);
        }

        public static bool FieldExists(SqlDataReader reader, string fieldName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
