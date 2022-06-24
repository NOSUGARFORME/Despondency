using Despondency.Application.Common.Services;
using Despondency.Domain.Entities;
using MediatR;

namespace Despondency.Application.Common.Commands;

public record AddOrUpdateEntityCommand<TEntity>(TEntity Entity) : IRequest
    where TEntity : AggregateRoot<Guid>;

internal class AddOrUpdateEntityCommandHandler<TEntity> : IRequestHandler<AddOrUpdateEntityCommand<TEntity>>
    where TEntity : AggregateRoot<Guid>
{
    private readonly ICrudService<TEntity> _crudService;
    
    public AddOrUpdateEntityCommandHandler(ICrudService<TEntity> crudService)
    {
        _crudService = crudService;
    }

    public async Task<Unit> Handle(AddOrUpdateEntityCommand<TEntity> request, CancellationToken cancellationToken)
    {
        await _crudService.AddOrUpdateAsync(request.Entity);
        return Unit.Value;
    }
}