using SCGR.Application.Common.Params;
using SCGR.Application.Common.Wrappers;
using SCGR.Application.Features.Transactions.Requests;
using SCGR.Application.Features.Transactions.Responses;
using SCGR.Domain.Abstractions.Types;

namespace SCGR.Application.Contracts.Services;

public interface ITransactionService
{
    Task<Result<Paged<TransactionResponseDto>>> GetAllTransactionsAsync(PaginationParams request);
    Task<Result<Paged<TransactionResponseDto>>> GetTransactionsByCategoryIdAsync(GetTransactionByCategoryQueryDto request);
    Task<Result<Paged<TransactionResponseDto>>> GetTransactionByDateRangeAsync(GetTransactionDateRangeQueryDto request);
    Task<Result<TransactionResponseDto>> GetTransactionByIdAsync(int id);
    Task<Result<TransactionCreatedResponseDto>> CreateTransactionAsync(TransactionCommandDto request);
    Task<Result> UpdateTransactionAsync(int id, TransactionCommandDto request);
    Task<Result> DeleteTransactionAsync(int id);
}
