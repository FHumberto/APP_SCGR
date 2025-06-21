using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SCGR.Application.Contracts.Persistence;
using SCGR.Domain.Abstractions.Interfaces;
using SCGR.Infrastructure.Persistence.Contexts;
using SCGR.Infrastructure.Repositories;

namespace SCGR.Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ScgrDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
