using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;

namespace Despondency.Application.Common.Queries;

public record GetEntityByIdQuery<TEntity>(Guid Id) : IRequest<TEntity>
    where TEntity : AggregateRoot<Guid>;
    
internal class GetEntityByIdQueryHandler<TEntity> : IRequestHandler<GetEntityByIdQuery<TEntity>, TEntity>
    where TEntity : AggregateRoot<Guid>
{
    private readonly IRepository<TEntity, Guid> _repository;

    public GetEntityByIdQueryHandler(IRepository<TEntity, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity> Handle(GetEntityByIdQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == query.Id));
        return entity;
    }
}
    