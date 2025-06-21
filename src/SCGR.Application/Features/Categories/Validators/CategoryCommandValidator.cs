using FluentValidation;
using SCGR.Application.Features.Categories.Requests;

namespace SCGR.Application.Features.Categories.Validators;

public class CategoryCommandValidator : AbstractValidator<CategoryCommandDto>
{
    public CategoryCommandValidator()
        => RuleFor(c => c.Name).ValidCategoryName();
}
