using Despondency.Domain.Entities;

namespace Despondency.Domain.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
{
    IUnitOfWork UnitOfWork { get; }
    IQueryable<TEntity> GetAll();
    Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);
    Task<List<T>> ToListAsync<T>(IQueryable<T> query);
    Task AddOrUpdateAsync(TEntity entity);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    void Delete(TEntity entity);
}
