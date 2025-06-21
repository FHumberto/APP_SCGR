using FluentValidation;
using FluentValidation.Results;
using SCGR.Application.Common.Helpers;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Contracts.Persistence;
using SCGR.Application.Contracts.Services;
using SCGR.Application.Features.Categories.Requests;
using SCGR.Application.Features.Categories.Responses;
using SCGR.Domain.Abstractions.Errors;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;

namespace SCGR.Application.Features.Categories;

public sealed class CategoryService : ICategoryService
{
    #region [ DEPENDÊNCIAS ]

    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CategoryCommandDto> _categoryCommandValidator;
    private readonly IValidator<CategoryQueryDto> _categoryQueryValidator;
    private readonly IValidator<PaginationParams> _paginationParamsValidator;

    public CategoryService
        (ICategoryRepository categoryRepository,
        IValidator<CategoryCommandDto> categoryCommandValidator,
        IValidator<CategoryQueryDto> categoryQueryValidator,
        IValidator<PaginationParams> paginationParamsValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryCommandValidator = categoryCommandValidator;
        _categoryQueryValidator = categoryQueryValidator;
        _paginationParamsValidator = paginationParamsValidator;
    }

    #endregion

    #region [ LEITURA ]

    public async Task<Result<Paged<CategoryResponseDto>>> GetAllCategoriesAsync(PaginationParams request)
    {
        ValidationResult validationResult = _paginationParamsValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<Paged<CategoryResponseDto>>.Failure(error);
        }

        Paged<Category>? categories = await _categoryRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);

        return Result<Paged<CategoryResponseDto>>.Success(categories.ToPagedResponseDto());
    }

    public async Task<Result<CategoryResponseDto>> GetCategoryByIdAsync(int id)
    {
        Category? category = await _categoryRepository.GetByIdAsync(id);

        return category is not null
            ? Result<CategoryResponseDto>.Success(category.ToResponseDto())
            : Result<CategoryResponseDto>.Failure(CategoryErrors.CategoryNotFound);
    }

    public async Task<Result<Paged<CategoryResponseDto>>> GetCategoryByNameAsync(CategoryQueryDto request)
    {
        ValidationResult validationResult = _categoryQueryValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<Paged<CategoryResponseDto>>.Failure(error);
        }

        Paged<Category>? categories = await _categoryRepository.GetByNamePagedAsync
            (request.Name, request.Pagination.PageNumber, request.Pagination.PageSize);

        return Result<Paged<CategoryResponseDto>>.Success(categories.ToPagedResponseDto());
    }

    #endregion

    #region [ GRAVAÇAO ]

    public async Task<Result<CategoryCreatedResponseDto>> CreateCategoryAsync(CategoryCommandDto request)
    {
        ValidationResult validationResult = _categoryCommandValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<CategoryCreatedResponseDto>.Failure(error);
        }

        Paged<Category> existing = await _categoryRepository.GetByNamePagedAsync(request.Name, 1, 1);

        if (existing.Items.Count > 0)
        {
            return Result<CategoryCreatedResponseDto>.Failure(CategoryErrors.CategoryAlreadyExists);
        }

        Category category = request.ToEntity();

        await _categoryRepository.CreateAsync(category);

        return Result<CategoryCreatedResponseDto>.Success(category.ToCreatedResponseDto());
    }

    public async Task<Result> UpdateCategoryAsync(int id, CategoryCommandDto request)
    {
        Category? existingCategory = await _categoryRepository.GetByIdAsync(id);

        if (existingCategory is null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        ValidationResult validationResult = _categoryCommandValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result.Failure(error);
        }

        existingCategory.Update(request.Name);

        await _categoryRepository.UpdateAsync(existingCategory);

        return Result.Success();
    }

    public async Task<Result> DeleteCategoryAsync(int id)
    {
        Category? category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        await _categoryRepository.DeleteAsync(category);
        return Result.Success();
    }

    #endregion
}
