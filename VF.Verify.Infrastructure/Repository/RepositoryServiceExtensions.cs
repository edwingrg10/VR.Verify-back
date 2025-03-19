using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VF.Verify.Domain.Interfaces.Repository.DataContext;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Infrastructure.Repository.Repositories;


namespace VF.Verify.Infrastructure.Repository
{
    public static class RepositoryServiceExtensions

    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IDataContext, DataContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IExcelRepository, ExcelRepository>();
            services.AddTransient<IRuleRepository, RuleRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IDistribuitorRepository, DistribuitorRepository>();
            services.AddTransient<IContryRepository, CountryRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            return services;
        }

    }
}
