using System;

namespace EventSourcingSample
{
    public class Account : EventSourcedAggregate
    {
        public Account()
        { }
        public Account(Guid id,string number, decimal balance)
        {
            Causes(new AccountCreatedEvent(id, number, balance));
        }

        public string Number { get; private set; }
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            Causes(new AccountDiposited(Id, amount));
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new Exception("amount is not valid!!!");
            }

            Causes(new AccountWithdrawn(Id, amount));
        }

        private void Causes(IDomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        private void When(AccountCreatedEvent domainEvent)
        {
            Id = domainEvent.Id;
            Balance = domainEvent.Balance;
            Number = domainEvent.Number;

        }

        private void When(AccountDiposited domainEvent)
        {
            Balance += domainEvent.Amount;
        }

        private void When(AccountWithdrawn domainEvent)
        {
            Balance -= domainEvent.Amount;
        }

        public override void Apply(IDomainEvent domainEvent)
        {
            
            When((dynamic)domainEvent);
            Version += 1;
        }
    }
}