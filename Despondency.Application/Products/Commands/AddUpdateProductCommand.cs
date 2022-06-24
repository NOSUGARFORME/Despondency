using Despondency.Application.Common.Services;
using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;

namespace Despondency.Application.Products.Commands;

public record AddUpdateProductCommand(Product Product) : IRequest;

public class AddUpdateProductCommandHandler : IRequestHandler<AddUpdateProductCommand>
{
    private readonly ICrudService<Product> _productService;
    private readonly IUnitOfWork _unitOfWork;

    public AddUpdateProductCommandHandler(ICrudService<Product> productService, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddUpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        await _productService.AddOrUpdateAsync(command.Product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}