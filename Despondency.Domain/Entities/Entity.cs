namespace Despondency.Domain.Entities;

public class Entity<TKey>
{
    public TKey Id { get; set; }
    
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
}
