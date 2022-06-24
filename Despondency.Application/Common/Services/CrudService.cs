using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;

namespace Despondency.Application.Common.Services;

public class CrudService<T> : ICrudService<T>
    where T : AggregateRoot<Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<T, Guid> _repository;

    public CrudService(IRepository<T, Guid> repository)
    {
        _unitOfWork = repository.UnitOfWork;
        _repository = repository;
    }
    
    public Task<List<T>> GetAsync()
    {
        return _repository.ToListAsync(_repository.GetAll());
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        return _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == id));
    }

    public async Task AddOrUpdateAsync(T entity)
    {
        await _repository.AddOrUpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}