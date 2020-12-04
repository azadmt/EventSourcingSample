using System;
using System.Collections.Generic;

namespace EventSourcingSample
{
    public abstract class EventSourcedAggregate
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public List<IDomainEvent> Changes { get; private set; }

        public EventSourcedAggregate()
        {
            Changes = new List<IDomainEvent>();
        }

        public abstract void Apply(IDomainEvent domainEvent);
    }
}