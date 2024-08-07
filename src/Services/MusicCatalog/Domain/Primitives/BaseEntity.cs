using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Primitives;

public class BaseEntity<TEntityId> : BaseEntity where TEntityId : struct
{ 
    public TEntityId Id { get; protected set; }
}

public abstract class BaseEntity
{
    private readonly List<BaseEvent> _events = [];

    [NotMapped]
    public ICollection<BaseEvent> Events => _events;
    
    protected void AddEvent(BaseEvent @event)
    {
        _events.Add(@event);
    }
}