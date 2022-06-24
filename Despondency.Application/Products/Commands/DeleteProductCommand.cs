using Despondency.Application.Common.Services;
using Despondency.Domain.Entities;
using MediatR;

namespace Despondency.Application.Products.Commands;

public record DeleteProductCommand(Product Product) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly ICrudService<Product> _productService;

    public DeleteProductCommandHandler(ICrudService<Product> productService)
    {
        _productService = productService;
    }

    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        await _productService.DeleteAsync(command.Product);
        return Unit.Value;
    }
}
