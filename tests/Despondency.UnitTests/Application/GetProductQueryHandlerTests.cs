using Despondency.Application.Products.Queries;
using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;
using NSubstitute;
using Shouldly;

namespace Despondency.UnitTests.Application;

public class GetProductQueryHandlerTests
{
    private readonly IRepository<Product, Guid> _productRepository;
    private readonly IRequestHandler<GetProductQuery, Product> _commandHandler;

    public GetProductQueryHandlerTests()
    {
        _productRepository = Substitute.For<IRepository<Product, Guid>>();
        _commandHandler = new GetProductQueryHandler(_productRepository);
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new GetProductQuery(Guid.NewGuid());
        _productRepository.FirstOrDefaultAsync(Arg.Any<IQueryable<Product>>())
            .Returns(Task.FromResult(new Product{Id = command.Id, Name = "name", Code = "code"}));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _productRepository.Received(1).FirstOrDefaultAsync(Arg.Any<IQueryable<Product>>());
    }
    
    private Task Act(GetProductQuery command)
        => _commandHandler.Handle(command, CancellationToken.None);
}