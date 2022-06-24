using Despondency.Application.Products.Commands;
using Despondency.Application.Products.Queries;
using Despondency.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Despondency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> Get(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));
        return Ok(product);
    }
    
    [HttpPost]
    [Consumes("application/json")]
    public async Task<ActionResult<Product>> Post([FromBody] Product model)
    {
        await _mediator.Send(new AddUpdateProductCommand(model));
        return Created($"/api/products/{model.Id}", model);
    }
    
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Product model)
    {
        var product = await _mediator.Send(new GetProductQuery(id));

        product.Code = model.Code;
        product.Name = model.Name;

        await _mediator.Send(new AddUpdateProductCommand(product));
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));

        await _mediator.Send(new DeleteProductCommand(product));
        return NoContent();
    }
}
