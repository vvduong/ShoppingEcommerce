using System.Collections.Generic;
using ShoppingEcommerce.Core.DomainEvent;

namespace ShoppingEcommerce.Core.Abstraction
{
    /// <inheritdoc />
    /// <summary>
    /// An aggregate will have one of its component objects be the aggregate root. 
    /// 
    /// Any references from outside the aggregate should only go to the aggregate root. 
    /// 
    /// The root can thus ensure the integrity of the aggregate as a whole.
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}