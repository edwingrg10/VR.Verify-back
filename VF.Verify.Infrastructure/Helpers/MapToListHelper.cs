using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;

public static class MapToListHelper
{
    public static List<T> MapToList<T>(IDataReader dataReader) where T : new()
    {
        var list = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        while (dataReader.Read())
        {
            var obj = new T();
            foreach (var prop in properties)
            {
                if (Attribute.IsDefined(prop, typeof(NotMappedAttribute)))
                {
                    continue;
                }

                var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;
                if (dataReader[jsonPropertyName] == DBNull.Value) continue;

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var safeValue = Convert.ChangeType(dataReader[jsonPropertyName], type);
                prop.SetValue(obj, safeValue, null);
            }
            list.Add(obj);
        }
        return list;
    }


    public static Task<List<T>> MapToListAsync<T>(IDataReader dataReader) where T : new()
    {
        return Task.Run(() =>
        {
            var list = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (dataReader.Read())
            {
                var obj = new T();
                foreach (var prop in properties)
                {
                    var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;

                    if (dataReader[jsonPropertyName] == DBNull.Value) continue;

                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var safeValue = Convert.ChangeType(dataReader[jsonPropertyName], type);
                    prop.SetValue(obj, safeValue, null);
                }
                list.Add(obj);
            }

            return list;
        });
    }
}
