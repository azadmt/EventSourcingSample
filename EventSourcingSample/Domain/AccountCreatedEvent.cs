using System;

namespace EventSourcingSample
{
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountCreatedEvent(Guid id, string number, decimal balance)
        {
            Id = id;
            Number = number;
            Balance = balance;
        }

        public Guid Id { get; private set; }
        public string Number { get; private set; }
        public decimal Balance { get; private set; }
    }
}