using FluentValidation;
using SCGR.Application.Common.Params;

namespace SCGR.Application.Common.Validation;

public class PaginationParamsValidator<T> : AbstractValidator<T> where T : PaginationParams
{
    public PaginationParamsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("O número da página deve ser pelo menos 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("O tamanho da página deve estar entre 1 e 100.");
    }
}