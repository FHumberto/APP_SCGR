using FluentValidation;
using FluentValidation.Results;
using SCGR.Application.Common.Helpers;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Contracts.Persistence;
using SCGR.Application.Contracts.Services;
using SCGR.Application.Features.Transactions.Requests;
using SCGR.Application.Features.Transactions.Responses;
using SCGR.Domain.Abstractions.Errors;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Features.Transactions;

public sealed class TransactionService : ITransactionService
{
    #region [ DEPENDÊNCIAS ]

    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<GetTransactionDateRangeQueryDto> _transactionDateRangeQueryValidator;
    private readonly IValidator<TransactionCommandDto> _transactionCommandValidator;
    private readonly IValidator<PaginationParams> _paginationParamsValidator;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IValidator<GetTransactionDateRangeQueryDto> transactionDateRangeQueryValidator,
        IValidator<TransactionCommandDto> transactionCommandValidator,
        IValidator<PaginationParams> paginationParamsValidator)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _transactionDateRangeQueryValidator = transactionDateRangeQueryValidator;
        _transactionCommandValidator = transactionCommandValidator;
        _paginationParamsValidator = paginationParamsValidator;
    }

    #endregion

    #region [ LEITURA ]

    public async Task<Result<Paged<TransactionResponseDto>>> GetAllTransactionsAsync(PaginationParams request)
    {
        ValidationResult validationResult = _paginationParamsValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<Paged<TransactionResponseDto>>.Failure(error);
        }

        Paged<Transaction> transactions = await _transactionRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return Result<Paged<TransactionResponseDto>>.Success(transactions.ToPagedResponseDto());
    }

    public async Task<Result<Paged<TransactionResponseDto>>> GetTransactionByDateRangeAsync(GetTransactionDateRangeQueryDto request)
    {
        ValidationResult validationResult = _transactionDateRangeQueryValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<Paged<TransactionResponseDto>>.Failure(error);
        }

        Paged<Transaction> transactions = await _transactionRepository.GetByDateRangeAsync(
            request.DateRange.StartDate,
            request.DateRange.EndDate,
            request.Pagination.PageNumber,
            request.Pagination.PageSize);
        return Result<Paged<TransactionResponseDto>>.Success(transactions.ToPagedResponseDto());
    }

    public async Task<Result<Paged<TransactionResponseDto>>> GetTransactionsByCategoryIdAsync(GetTransactionByCategoryQueryDto request)
    {
        ValidationResult validationResult = _paginationParamsValidator.Validate(request.Pagination!);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<Paged<TransactionResponseDto>>.Failure(error);
        }

        Category? categoryExists = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (categoryExists == null)
        {
            return Result<Paged<TransactionResponseDto>>.Failure(CategoryErrors.CategoryNotFound);
        }

        Paged<Transaction> transactions = await _transactionRepository.GetByCategoryIdAsync(
            request.CategoryId,
            request.Pagination.PageNumber,
            request.Pagination.PageSize);

        return Result<Paged<TransactionResponseDto>>.Success(transactions.ToPagedResponseDto());
    }

    public async Task<Result<TransactionResponseDto>> GetTransactionByIdAsync(int id)
    {
        Transaction? transaction = await _transactionRepository.GetByIdAsync(id);
        return transaction is not null
            ? Result<TransactionResponseDto>.Success(transaction.ToResponseDto())
            : Result<TransactionResponseDto>.Failure(TransactionErrors.TransactionNotFound);
    }

    #endregion

    #region [ GRAVAÇÃO ]

    public async Task<Result<TransactionCreatedResponseDto>> CreateTransactionAsync(TransactionCommandDto request)
    {
        ValidationResult validationResult = await _transactionCommandValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result<TransactionCreatedResponseDto>.Failure(error);
        }

        Transaction transaction = request.ToEntity();

        await _transactionRepository.CreateAsync(transaction);

        return Result<TransactionCreatedResponseDto>.Success(transaction.ToCreatedResponseDto());
    }

    public async Task<Result> UpdateTransactionAsync(int id, TransactionCommandDto request)
    {
        ValidationResult validationResult = await _transactionCommandValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            Error error = ValidationHelper.ToValidationError(validationResult);
            return Result.Failure(error);
        }

        Transaction? transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction is null)
        {
            return TransactionErrors.TransactionNotFound;
        }

        await _transactionRepository.UpdateAsync(transaction);
        return Result.Success();
    }

    public async Task<Result> DeleteTransactionAsync(int id)
    {
        Transaction? transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction is null)
        {
            return TransactionErrors.TransactionNotFound;
        }

        await _transactionRepository.DeleteAsync(transaction);
        return Result.Success();
    }

    #endregion
}