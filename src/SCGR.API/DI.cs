using SCGR.API.Middlewares;

namespace SCGR.API;

public static class DI
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionMiddleware>();
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
