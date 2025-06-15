namespace SCGR.API;

public static class DI
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
