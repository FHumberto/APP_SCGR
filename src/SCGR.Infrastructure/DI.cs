using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SCGR.Infrastructure.Persistence.Contexts;

namespace SCGR.Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ScgrDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
