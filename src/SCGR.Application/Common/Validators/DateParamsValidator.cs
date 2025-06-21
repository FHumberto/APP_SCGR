using FluentValidation;
using SCGR.Application.Common.Params;

namespace SCGR.Application.Common.Validators;

public class DateParamsValidator<T> : AbstractValidator<T> where T : DateParams
{
    public DateParamsValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Data de início é obrigatória.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Data de fim é obrigatória.");

        RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("Data de fim deve ser posterior a data de início.");
    }
}