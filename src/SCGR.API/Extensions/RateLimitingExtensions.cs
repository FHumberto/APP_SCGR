using SCGR.Application.Common.Constants;
using System.Threading.RateLimiting;

namespace SCGR.API.Extensions;

public static class RateLimiterExtension
{
    /// <summary>
    /// Adiciona a politica de Rate Limiter configurada à coleção de serviços da aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços da aplicação.</param>
    /// <returns>A <see cref="IServiceCollection"/> de serviços da aplicação com a política de Reate Limiter adicionada.</returns>
    public static IServiceCollection AddRateLimiterPolicies(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext
                => RateLimitPartition.GetFixedWindowLimiter(RateLimiterPolicies.Fixed, _
                    => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }));

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsJsonAsync
                (
                    new { message = "Você excedeu o limite de requisições. Tente novamente mais tarde." },
                    cancellationToken
                );
            };
        });

        return services;
    }
}
