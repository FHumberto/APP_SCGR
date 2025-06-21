using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Features.Transactions.Responses;

public sealed class TransactionResponseDto
{
    public int Id { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateOnly TransactionDate { get; set; }
    public int CategoryId { get; set; }

    public TransactionResponseDto(int id, TransactionType transactionType, string? description, decimal amount, DateOnly transactionDate, int categoryId)
    {
        Id = id;
        TransactionType = transactionType;
        Description = description;
        Amount = amount;
        TransactionDate = transactionDate;
        CategoryId = categoryId;
    }
}