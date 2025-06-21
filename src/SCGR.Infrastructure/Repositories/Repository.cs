using Microsoft.EntityFrameworkCore;
using SCGR.Domain.Abstractions.Interfaces;
using SCGR.Domain.Abstractions.Types;
using SCGR.Infrastructure.Persistence.Contexts;

namespace SCGR.Infrastructure.Repositories;

public class Repository<T>(ScgrDbContext context) : IRepository<T> where T : Entity
{
    #region [ DEPENDÊNCIAS ]

    protected readonly ScgrDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    #endregion

    #region [ LEITURA ]

    public async Task<Paged<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();
        return await GetAllQueryPagedAsync(query, pageNumber, pageSize);
    }

    public async Task<Paged<T>> GetAllQueryPagedAsync(IQueryable<T> query, int pageNumber, int pageSize)
    {
        int totalRecords = await query.CountAsync();

        List<T> items = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new Paged<T>(items, totalRecords, pageNumber, pageSize);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Set<T>().AnyAsync(e => e.Id == id);

    #endregion

    #region [ GRAVAÇÃO ]

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    #endregion
}