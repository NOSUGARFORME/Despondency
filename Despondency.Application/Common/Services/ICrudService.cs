using Despondency.Domain.Entities;

namespace Despondency.Application.Common.Services;

public interface ICrudService<T>
    where T : AggregateRoot<Guid>
{
    Task<List<T>> GetAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddOrUpdateAsync(T entity);
    Task DeleteAsync(T entity);
}