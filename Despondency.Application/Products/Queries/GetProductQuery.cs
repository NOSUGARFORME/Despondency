using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;

namespace Despondency.Application.Products.Queries;

public record GetProductQuery(Guid Id) : IRequest<Product>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    private readonly IRepository<Product, Guid> _productRepository;

    public GetProductQueryHandler(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FirstOrDefaultAsync(
            _productRepository.GetAll().Where(x => x.Id == request.Id));

        return product;
    }
}