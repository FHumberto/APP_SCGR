using Microsoft.EntityFrameworkCore;
using SCGR.Application.Contracts.Persistence;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Transactions;
using SCGR.Infrastructure.Persistence.Contexts;

namespace SCGR.Infrastructure.Repositories;

public sealed class TransactionRepository(ScgrDbContext dbContext) : Repository<Transaction>(dbContext), ITransactionRepository
{
    #region [ LEITURA ] 

    public async Task<Paged<Transaction>> GetByCategoryIdAsync(int categoryId, int pageNumber, int pageSize)
    {
        IQueryable<Transaction> query = _context.Transactions
            .Where(t => t.CategoryId == categoryId)
            .AsNoTracking();

        return await GetAllQueryPagedAsync(query, pageNumber, pageSize);
    }

    public async Task<Paged<Transaction>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate, int pageNumber, int pageSize)
    {
        IQueryable<Transaction> query = _context.Transactions
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
            .OrderByDescending(t => t.TransactionDate)
            .AsNoTracking();

        return await GetAllQueryPagedAsync(query, pageNumber, pageSize);
    }

    public async Task<Paged<Transaction>> GetByTransactionTypeAsync(TransactionType transactionType, int pageNumber, int pageSize)
    {
        IQueryable<Transaction> query = _context.Transactions
            .Where(t => t.TransactionType == transactionType)
            .OrderByDescending(t => t.TransactionDate)
            .AsNoTracking();

        return await GetAllQueryPagedAsync(query, pageNumber, pageSize);
    }

    #endregion
}
