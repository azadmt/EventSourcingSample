using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingSample.Application
{
    public class AccountService
    {
        AccountRepository accountRepository;
        public AccountService()
        {
            accountRepository = new AccountRepository(new EventStore());
        }

        public void Handle(CreateAccountCommand command)
        {
            var account = new Account(command.AccountId, $"{DateTime.Now.Year}{DateTime.Now.Minute}{DateTime.Now.Second}", command.InitialBalance);
            accountRepository.Add(account);
        }


        public void Handle(DepositToAccountCommand command)
        {
            var account = accountRepository.Get(command.AccountId);
            account.Deposit(command.Amount);
            accountRepository.Save(account);
        }
    }
}
