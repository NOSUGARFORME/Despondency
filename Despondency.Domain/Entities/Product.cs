namespace Despondency.Domain.Entities;

public class Product : AggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
}
