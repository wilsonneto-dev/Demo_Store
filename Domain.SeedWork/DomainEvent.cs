namespace Domain.SeedWork;

public abstract class DomainEvent
{
    public Guid EventId { get; private set; } = Guid.NewGuid();
    public DateTime OccurredOn { get; private set; } = DateTime.Now;
}