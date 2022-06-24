using Despondency.Application.Common.Services;
using Despondency.Application.Products.Commands;
using Despondency.Domain.Entities;
using Despondency.Domain.Repositories;
using MediatR;
using NSubstitute;
using Shouldly;

namespace Despondency.UnitTests.Application;

public class UpdateProductHandlerTests
{
    private readonly IRequestHandler<AddUpdateProductCommand> _handler;
    private readonly ICrudService<Product> _crudService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandlerTests()
    {
        _crudService = Substitute.For<ICrudService<Product>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _handler = new AddUpdateProductCommandHandler(_crudService, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Calls_Repository_On_Success()
    {
        var command = new AddUpdateProductCommand(new Product {Id = Guid.NewGuid(), Code = "code", Name = "Name"});
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _crudService.Received(1).AddOrUpdateAsync(Arg.Any<Product>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }
    
    private Task Act(AddUpdateProductCommand command)
        => _handler.Handle(command, CancellationToken.None);
}