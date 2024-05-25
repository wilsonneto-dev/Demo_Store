namespace Domain.SeedWork;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _events = [];

    public void RaiseEvent(DomainEvent domainEvent) => 
        _events.Add(domainEvent);
    
    public IReadOnlyList<DomainEvent> GetEvents() => 
        new List<DomainEvent>(_events).AsReadOnly();
    
    public void ClearEvents() => _events.Clear();
}