using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShoppingEcommerce.Core.DomainEvent
{
    public class DomainEventDispatcher
    {
        private readonly IEnumerable<Type> _domainEventhandlers;

        public DomainEventDispatcher()
        {
            _domainEventhandlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type
                    .GetInterfaces()
                    .Any(iType => iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
                .ToList();
        }

        public void Dispatch(IDomainEvent domainEvent)
        {
            foreach (var domainEventHandler in _domainEventhandlers)
            {
                var canHandleEvent = domainEventHandler
                    .GetInterfaces()
                    .Any(type => type.IsGenericType
                        && type.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)
                        && type.GenericTypeArguments.FirstOrDefault() == domainEvent.GetType());

                if (!canHandleEvent)
                {
                    continue;
                }

                dynamic handler = Activator.CreateInstance(domainEventHandler);

                handler.Handle((dynamic)domainEvent);
            }
        }
    }
}