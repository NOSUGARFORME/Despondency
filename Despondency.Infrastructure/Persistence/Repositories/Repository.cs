using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Despondency.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
{
    private readonly AppDbContext _dbContext;
    private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public IUnitOfWork UnitOfWork => _dbContext;
    
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }
    
    public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
    {
        return query.FirstOrDefaultAsync();
    }

    public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
    {
        return query.ToListAsync();
    }

    public async Task AddOrUpdateAsync(TEntity entity)
    {
        if (entity.Id != null && entity.Id.Equals(default(TKey)))
        {
            await AddAsync(entity);
        }
        else
        {
            await UpdateAsync(entity);
        }
    }

    public async Task AddAsync(TEntity entity)
    {
        entity.CreatedDateTime = DateTimeOffset.UtcNow;
        await DbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        entity.UpdatedDateTime = DateTimeOffset.UtcNow;
        return Task.CompletedTask;
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}
