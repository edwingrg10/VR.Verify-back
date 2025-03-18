using Microsoft.Data.SqlClient;

namespace VF.Verify.Domain.Interfaces.Services
{
    public interface ISqlCommandService
    {
        void AddParameters<T>(SqlCommand command, T parameters);
    }

}
