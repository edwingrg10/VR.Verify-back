using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Infrastructure.UseCases
{
    public static class BLLServiceExtensions

    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IUserUseCase, UserUseCase>();
            services.AddTransient<IExcelUseCase, ExcelUseCase>();
            services.AddTransient<IRuleUseCase, RuleUseCase>();
            return services;
        }

    }
}
