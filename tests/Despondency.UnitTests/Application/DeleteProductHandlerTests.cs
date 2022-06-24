using Despondency.Application.Common.Services;
using Despondency.Application.Products.Commands;
using Despondency.Domain.Entities;
using MediatR;
using NSubstitute;
using Shouldly;

namespace Despondency.UnitTests.Application;

public class DeleteProductHandlerTests
{
    private readonly IRequestHandler<DeleteProductCommand> _handler;
    private readonly ICrudService<Product> _crudService;

    public DeleteProductHandlerTests()
    {
        _crudService = Substitute.For<ICrudService<Product>>();

        _handler = new DeleteProductCommandHandler(_crudService);
    }

    [Fact]
    public async Task Handle_Calls_Repository_On_Success()
    {
        var command = new DeleteProductCommand(new Product {Id = Guid.NewGuid(), Code = "code", Name = "Name"});
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _crudService.Received(1).DeleteAsync(Arg.Any<Product>());
    }
    
    private Task Act(DeleteProductCommand command)
        => _handler.Handle(command, CancellationToken.None);
}