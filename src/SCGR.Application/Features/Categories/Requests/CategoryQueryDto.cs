using SCGR.Application.Common.Params;

namespace SCGR.Application.Features.Categories.Requests;

public sealed class CategoryQueryDto
{
    public required string Name { get; set; } = string.Empty;
    public required PaginationParams Pagination { get; set; }
}
