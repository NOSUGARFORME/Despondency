using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;

namespace Despondency.Application.Products.Queries;

public record GetProductsQuery : IRequest<List<Product>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IRepository<Product, Guid> _productRepository;

    public GetProductsQueryHandler(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.ToListAsync(_productRepository.GetAll());
    }
}
