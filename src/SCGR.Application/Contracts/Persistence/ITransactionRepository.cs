using SCGR.Domain.Abstractions.Interfaces;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Contracts.Persistence;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<Paged<Transaction>> GetByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
    Task<Paged<Transaction>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate, int pageNumber, int pageSize);
    Task<Paged<Transaction>> GetByTransactionTypeAsync(TransactionType transactionType, int pageNumber, int pageSize);
}
