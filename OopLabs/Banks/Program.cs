using System;
using System.Threading;
using Banks.Model.Accounts;
using Banks.Model.Banks;
using Banks.Model.Clients;

namespace Banks
{
    class Program
    {
        static void Main(String[] args)
        {
            var _bank = new Bank("Tinkoff Bank");
            var _client = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9").SetPassport("1234567890").GetClient();
            var _creditAccount = new CreditAccount(_client, 2000, 4000, 2);
            _bank.AddClient(_client);
            _bank.AddAccountToClient(_client, _creditAccount);

            _bank.Withdraw(_creditAccount.Id, 3000);
            Console.WriteLine(_creditAccount.Balance);
        }
    }
}
