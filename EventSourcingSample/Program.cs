using EventSourcingSample.Application;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountService = new AccountService();
            var accountRepository= new AccountRepository(new EventStore());
            Console.WriteLine("Create Account with initialbalance 100...");
            var accountId = Guid.NewGuid();
            accountService.Handle(new CreateAccountCommand {AccountId= accountId, InitialBalance=100});
            Console.WriteLine("when Deposit to Account  1000...");
            accountService.Handle(new DepositToAccountCommand {AccountId= accountId, Amount = 1000 });
            Console.WriteLine("when Deposit to Account  2000...");
            accountService.Handle(new DepositToAccountCommand { AccountId = accountId, Amount = 2000 });

            Console.WriteLine("then my Account balance must be  3100...");
            var account = accountRepository.Get(accountId);

            Console.WriteLine($" Account balance is {account.Balance}");
            Console.ReadKey();
        }
    }
}
