using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Validators;
using SCGR.Application.Contracts.Services;
using SCGR.Application.Features.Categories;
using SCGR.Application.Features.Transactions;

namespace SCGR.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        //? registra validadores genéricos
        services.AddScoped<IValidator<PaginationParams>, PaginationParamsValidator<PaginationParams>>();
        services.AddScoped<IValidator<DateParams>, DateParamsValidator<DateParams>>();

        //? registra todos os validadores concretos
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
