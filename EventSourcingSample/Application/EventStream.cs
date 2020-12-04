namespace EventSourcingSample.Application
{
    public class EventWrapper
    {
        public string Id { get; private set; }
        public IDomainEvent Event { get; private set; }
        public string EventStreamId { get; private set; }
        public int EventNumber { get; private set; }

        public EventWrapper(IDomainEvent @event, int eventNumber, string eventStreamId)
        {
            Event = @event;
            EventNumber = eventNumber;
            EventStreamId = eventStreamId;
            Id = $"{eventStreamId}-{eventNumber}";
        }


    }

    public class EventStream
    {
        public int Version { get; set; }
        public string Id { get; set; }// Aggregate Type + Id

        public EventStream(string id)
        {
            Id = id;
            Version = 0;
        }


        public EventWrapper RegisEvent(IDomainEvent @event)
        {
            Version++;
            return new EventWrapper(@event, Version, Id);
        }
    }
}