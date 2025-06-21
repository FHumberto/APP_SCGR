using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Features.Categories.Requests;
using SCGR.Application.Features.Categories.Responses;
using SCGR.Domain.Abstractions.Types;

namespace SCGR.Application.Contracts.Services;

public interface ICategoryService
{
    Task<Result<Paged<CategoryResponseDto>>> GetAllCategoriesAsync(PaginationParams pagination);
    Task<Result<CategoryResponseDto>> GetCategoryByIdAsync(int id);
    Task<Result<Paged<CategoryResponseDto>>> GetCategoryByNameAsync(CategoryQueryDto query);
    Task<Result<CategoryCreatedResponseDto>> CreateCategoryAsync(CategoryCommandDto command);
    Task<Result> UpdateCategoryAsync(int id, CategoryCommandDto command);
    Task<Result> DeleteCategoryAsync(int id);
}
