namespace Thinkland.Domain.Shared;

public class BaseEntity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class BaseEntity : BaseEntity<Guid>
{
}