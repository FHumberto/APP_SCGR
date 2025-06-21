using SCGR.Application.Features.Categories.Requests;
using SCGR.Application.Features.Categories.Responses;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;

namespace SCGR.Application.Features.Categories;

public static class CategoryMap
{
    public static CategoryResponseDto ToResponseDto(this Category category)
        => new(category.Id, category.Name);

    public static Paged<CategoryResponseDto> ToPagedResponseDto(this Paged<Category> pagedCategory)
    {
        return new Paged<CategoryResponseDto>
        (
            pagedCategory.Items.Select(c => c.ToResponseDto()).ToList(),
            pagedCategory.TotalRecords,
            pagedCategory.PageNumber,
            pagedCategory.PageSize
        );
    }

    public static CategoryCreatedResponseDto ToCreatedResponseDto(this Category category)
    => new(category.Id);

    public static Category ToEntity(this CategoryCommandDto request)
        => new(request.Name);
}
