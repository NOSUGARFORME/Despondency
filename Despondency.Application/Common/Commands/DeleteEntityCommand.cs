using Despondency.Application.Common.Services;
using Despondency.Domain.Entities;
using MediatR;

namespace Despondency.Application.Common.Commands;

public record DeleteEntityCommand<TEntity>(TEntity Entity) : IRequest
    where TEntity : AggregateRoot<Guid>;

public class DeleteEntityCommandHandler<TEntity> : IRequestHandler<DeleteEntityCommand<TEntity>> 
    where TEntity : AggregateRoot<Guid>
{
    private readonly ICrudService<TEntity> _crudService;

    public DeleteEntityCommandHandler(ICrudService<TEntity> crudService)
    {
        _crudService = crudService;
    }
    
    public async Task<Unit> Handle(DeleteEntityCommand<TEntity> request, CancellationToken cancellationToken)
    {
        await _crudService.DeleteAsync(request.Entity);
        return Unit.Value;
    }
}
