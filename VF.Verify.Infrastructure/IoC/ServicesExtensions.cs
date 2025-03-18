using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VF.Verify.Infrastructure.Repository;
using VF.Verify.Infrastructure.Services;
using VF.Verify.Infrastructure.UseCases;


namespace VF.Verify.Infrastructure.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDigitalWorkshopDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddRepositories(configuration);
            services.AddUseCases(configuration);
            services.AddServices();
            return services;
        }
    }

}
