using SCGR.Application.Features.Transactions.Requests;
using SCGR.Application.Features.Transactions.Responses;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Features.Transactions;

public static class TransactionMap
{
    public static TransactionResponseDto ToResponseDto(this Transaction transaction)
    {
        return new
            (
                transaction.Id,
                transaction.TransactionType,
                transaction.Description,
                transaction.Amount,
                transaction.TransactionDate,
                transaction.CategoryId
            );
    }

    public static Paged<TransactionResponseDto> ToPagedResponseDto(this Paged<Transaction> pagedTransaction)
    {
        return new Paged<TransactionResponseDto>
        (
            [.. pagedTransaction.Items.Select(t => t.ToResponseDto())],
            pagedTransaction.TotalRecords,
            pagedTransaction.PageNumber,
            pagedTransaction.PageSize
        );
    }

    public static TransactionCreatedResponseDto ToCreatedResponseDto(this Transaction transaction)
    => new(transaction.Id);

    public static Transaction ToEntity(this TransactionCommandDto request)
    {
        return new Transaction
        (
            request.TransactionType,
            request.Description,
            request.Amount,
            request.TransactionDate,
            request.CategoryId
        );
    }
}
