using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;

namespace Despondency.Application.Common.Queries;

public record GetEntitiesQuery<TEntity>() : IRequest<List<TEntity>>
    where TEntity : AggregateRoot<Guid>;
    
internal class GetEntitiesQueryHandler<TEntity> : IRequestHandler<GetEntitiesQuery<TEntity>, List<TEntity>>
    where TEntity : AggregateRoot<Guid>
{
    private readonly IRepository<TEntity, Guid> _repository;

    public GetEntitiesQueryHandler(IRepository<TEntity, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<List<TEntity>> Handle(GetEntitiesQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await _repository.ToListAsync(_repository.GetAll());
    }
}