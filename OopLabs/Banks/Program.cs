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
            var bank = new Bank("Tinkoff Bank");
            var client = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9").GetClient();
            var debitAccount = new DebitAccount(client, 10000, 2);
            bank.AddClient(client);
            bank.AddAccountToClient(client, debitAccount);

            Console.WriteLine(debitAccount.Balance);
            bank.BankTime.CurrentDate = DateTime.Now.AddDays(1);
            bank.Put(debitAccount.Id, 100);
            Console.WriteLine(debitAccount.Balance);
            bank.BankTime.CurrentDate = bank.BankTime.CurrentDate.AddDays(16);
            bank.Put(debitAccount.Id, 100);
            bank.UpdateAccountBalance();
            Console.WriteLine(debitAccount.Balance);
        }
    }
}
