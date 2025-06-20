using SCGR.Domain.Abstractions.Types;

namespace SCGR.Domain.Abstractions.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<Paged<T>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}