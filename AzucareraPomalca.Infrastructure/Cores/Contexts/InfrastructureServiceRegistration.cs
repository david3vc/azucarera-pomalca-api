using AzucareraPomalca.Domain.Cores.Paginations;
using AzucareraPomalca.Infrastructure.Cores.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzucareraPomalca.Infrastructure.Cores.Contexts
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection addInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });


            services.AddTransient(typeof(IPaginator<>), typeof(Paginator<>));

            return services;
        }
    }
}
