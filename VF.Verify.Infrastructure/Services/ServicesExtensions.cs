using Microsoft.Extensions.DependencyInjection;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Services
{
    public static class ServicesExtensions

    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISqlCommandService, SqlCommandServices>();
            services.AddTransient<IExecuteStoredProcedureService, ExecuteStoredProcedureService>();
            services.AddTransient<ILogService, LogService>();
            return services;
        }

    }
}
