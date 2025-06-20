using SCGR.Domain.Abstractions.Interfaces;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;

namespace SCGR.Application.Contracts.Persistence;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Paged<Category>> GetByNamePagedAsync(string name, int pageNumber, int pageSize);
}
