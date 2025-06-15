using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SCGR.API.Extensions;

public static class SwaggerExtension
{
    /// <summary>
    /// Adiciona o Swagger com versionamento a coleção de serviços da aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços da aplicação.</param>
    /// <returns>A <see cref="IServiceCollection"/> de serviços da aplicação com o Swagger adicionado.</returns>
    public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
    {
        #region [ ROTAS ]

        services.AddApiVersioning(
            setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(
                setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });

        #endregion

        #region [ CONFIGURAÇÃO ]

        services.AddSwaggerGen(setup => setup.EnableAnnotations()); //? habilita o [SwaggerOperation]

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        #endregion

        return services;
    }

    /// <summary>
    /// Configura as opções do SwaggerGen para cada versão da API.
    /// </summary>
    /// <param name="provider">Provedor para descrições de versão de API.</param>
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                //? cria uma nova descrição do Swagger para cada versão da API
                OpenApiInfo info = new()
                {
                    Title = Assembly.GetEntryAssembly()?.GetName().Name ?? "API",
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated)
                {
                    info.Description += " Esta Versão da API está obsoleta.";
                }

                options.SwaggerDoc(description.GroupName, info);
            }
        }

        public void Configure(string? name, SwaggerGenOptions options) => Configure(options);
    }

    /// <summary>
    /// Aplica as configurações do Swagger aos serviço adicionado.
    /// </summary>
    /// <param name="app">O construtor da aplicação.</param>
    /// <returns>O <see cref="IApplicationBuilder"/> com o Swagger aplicado.</returns>
    public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
    {
        // Obtém o provedor de descrição de versão da API para iterar sobre as versões
        IApiVersionDescriptionProvider provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();

        app.UseSwaggerUI(
            options =>
            {
                // csonfigura um endpoint no Swagger UI para cada versão da API
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

        return app;
    }
}
