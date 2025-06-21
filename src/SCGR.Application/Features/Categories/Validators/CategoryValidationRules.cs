using FluentValidation;

namespace SCGR.Application.Features.Categories.Validators;

public static class CategoryValidationRules
{
    public static IRuleBuilderOptions<T, string> ValidCategoryName<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MinimumLength(3).WithMessage("O nome da categoria deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome da categoria deve ter no máximo 100 caracteres.")
            .Matches("^[^0-9]*$").WithMessage("O nome da categoria não pode conter números.")
            .Matches(@"^[A-Za-zÀ-ÿ\s]+$").WithMessage("O nome da categoria deve conter apenas letras e espaços.");
    }
}
