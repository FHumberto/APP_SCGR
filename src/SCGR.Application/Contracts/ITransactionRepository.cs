using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Application.Contracts;

public interface ITransactionRepository
{
    Task<Paged<Transaction>> GetAllAsync();
    Task<Transaction> GetByIdAsync(int id);
    Task<Paged<Transaction>> GetByCategoryIdAsync(int categoryId);
    Task<Paged<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> ExistsAsync(int id);
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    Task<bool> DeleteAsync(int id);
}
