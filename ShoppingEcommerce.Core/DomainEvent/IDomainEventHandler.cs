namespace ShoppingEcommerce.Core.DomainEvent
{
    internal interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Handle(TDomainEvent domainEvent);
    }
}
