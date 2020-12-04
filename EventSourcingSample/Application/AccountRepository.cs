using System;

namespace EventSourcingSample.Application
{
    public class AccountRepository
    {
        private EventStore _eventStore;
        public AccountRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public Account Get(Guid aggregateId)
        {

            var streamName = GetStreamName(aggregateId);
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            //var snapshot = _eventStore.GetStream(GetStreamName(aggregateId));
            //if (snapshot != null)
            //{
            //    fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            //}

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            Account account = null;
            //if (snapshot != null)
            //{
            //    payAsYouGoAccount = new PayAsYouGoAccount(snapshot);
            //}
            //else
            //{
                account = new Account();
           // }


            foreach (var @event in stream)
            {
                account.Apply(@event);
            }

            return account;
            
        }

        public void Add(Account aggregate)
        {
            
            _eventStore.CreateNewStream(GetStreamName(aggregate.Id), aggregate.Changes);
        }

        public void Save(Account aggregate)
        {
            _eventStore.AppendEvenetsToStream(GetStreamName(aggregate.Id), aggregate.Changes);
        }

        private string GetStreamName(Guid id)
        {
            return nameof(Account) + "-" + id.ToString();
        }
    }
}
