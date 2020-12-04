using System;

namespace EventSourcingSample
{
    public class AccountWithdrawn : IDomainEvent
    {
        public AccountWithdrawn(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }

        public Guid AccountId { get; private set; }
        public decimal Amount { get; private set; }
    }
}