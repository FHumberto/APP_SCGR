using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Features.Transactions.Requests;

public sealed class TransactionCommandDto
{
    public TransactionType TransactionType { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0.0m;
    public DateOnly TransactionDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int CategoryId { get; set; } = 0;
}