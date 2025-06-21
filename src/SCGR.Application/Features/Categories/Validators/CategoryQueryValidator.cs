using FluentValidation;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Validators;
using SCGR.Application.Features.Categories.Requests;

namespace SCGR.Application.Features.Categories.Validators;

public class CategoryQueryValidator : AbstractValidator<CategoryQueryDto>
{
    public CategoryQueryValidator()
    {
        RuleFor(c => c.Name)
            .ValidCategoryName();

        RuleFor(c => c.Pagination)
            .SetValidator(new PaginationParamsValidator<PaginationParams>());
    }
}
