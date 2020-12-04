using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingSample.Application
{
    public class EventStore : IEventStore
    {
        public void CreateNewStream(string streamName, IList<IDomainEvent> domianEvents)
        {
            var eventStream = new EventStream(streamName);
            DocumentStore.Store(eventStream);
           
            AppendEvenetsToStream(streamName, domianEvents);
        }

        public void AppendEvenetsToStream(string streamName, IList<IDomainEvent> domianEvents)
        {
            var eventStream = DocumentStore.GetStream(streamName);
            foreach (var domianEvent in domianEvents)
            {
                var eventWrapper = eventStream.RegisEvent(domianEvent);
                DocumentStore.Store(eventWrapper);
            }

        }

        public IEnumerable<IDomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            var eventWrappers = DocumentStore.GetWrappers(streamName, fromVersion, toVersion).OrderBy(p => p.EventNumber);
            var events = new List<IDomainEvent>();
            foreach (var eventWrapper in eventWrappers)
            {
                events.Add(eventWrapper.Event);
            }
            return events;
        }
    }
}
