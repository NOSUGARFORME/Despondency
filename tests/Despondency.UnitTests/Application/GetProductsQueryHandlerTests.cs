using Despondency.Application.Products.Queries;
using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;
using NSubstitute;
using Shouldly;

namespace Despondency.UnitTests.Application;

public class GetProductsQueryHandlerTests
{
    private readonly IRepository<Product, Guid> _productRepository;
    private readonly IRequestHandler<GetProductsQuery, List<Product>> _commandHandler;

    public GetProductsQueryHandlerTests()
    {
        _productRepository = Substitute.For<IRepository<Product, Guid>>();
        _commandHandler = new GetProductsQueryHandler(_productRepository);
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new GetProductsQuery();
        _productRepository.ToListAsync(Arg.Any<IQueryable<Product>>())
            .Returns(Task.FromResult(new List<Product>{new Product{Id = Guid.NewGuid(), Name = "name", Code = "code"}}));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _productRepository.Received(1).ToListAsync(Arg.Any<IQueryable<Product>>());
    }
    
    private Task Act(GetProductsQuery command)
        => _commandHandler.Handle(command, CancellationToken.None);
}