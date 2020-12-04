using System;

namespace EventSourcingSample.Application
{
    public class CreateAccountCommand
    {
        public decimal InitialBalance { get; set; }
        public Guid AccountId { get; set; }
    }

    public class DepositToAccountCommand
    {
        public Guid AccountId{ get; set; }
        public decimal Amount { get; set; }
    }
}
