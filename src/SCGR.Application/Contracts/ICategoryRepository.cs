using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;

namespace SCGR.Application.Contracts;

public interface ICategoryRepository
{
    Task<Paged<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task<Category> GetByNameAsync(string name);
    Task<bool> ExistsAsync(int id);
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
}
