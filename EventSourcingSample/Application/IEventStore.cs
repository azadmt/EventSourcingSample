using System.Collections.Generic;

namespace EventSourcingSample.Application
{
    public interface IEventStore
    {
        void CreateNewStream(string streamName, IList<IDomainEvent> domianEvents);
        void AppendEvenetsToStream(string streamName, IList<IDomainEvent> domianEvents);
    }
}
