using Microsoft.EntityFrameworkCore;
using SCGR.Application.Contracts.Persistence;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;
using SCGR.Infrastructure.Persistence.Contexts;

namespace SCGR.Infrastructure.Repositories;

public sealed class CategoryRepository(ScgrDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public async Task<Paged<Category>> GetByNamePagedAsync(string name, int pageNumber, int pageSize)
    {
        IQueryable<Category> query = _context.Categories
            .AsNoTracking()
            .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
            .OrderByDescending(c => c.CreatedAt);

        return await GetAllQueryPagedAsync(query, pageNumber, pageSize);
    }
}
