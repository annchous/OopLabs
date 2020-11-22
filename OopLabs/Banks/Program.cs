using System;
using System.Linq;
using Banks.Model;
using Banks.Model.Accounts;

namespace Banks
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = Client
                .Builder("Владимир", "Путин")
                .SetAddress("Кремль").SetPassport("1234567890");
            var client1 = Client
                .Builder("Алексей", "Навальный")
                .SetPassport("0000000000")
                .GetClient();
            
            Bank tinkoffBank = new Bank("Tinkoff Bank");
            tinkoffBank.AddClient(client);
            var debit = new DepositAccount(client, 15000, TimeSpan.Zero);
            tinkoffBank.AddAccountToClient(client, debit);
            tinkoffBank.AddClient(client1);
            var debit1 = new DebitAccount(client1, 5000, 2);
            tinkoffBank.AddAccountToClient(client1, debit1);

            tinkoffBank.Transfer(debit.Id, debit1.Id, 5000);
            Console.WriteLine(debit.ToString());
            Console.WriteLine(debit1.ToString());

            tinkoffBank.Put(debit.Id, 12345004);
            Console.WriteLine(debit.ToString());

            tinkoffBank.CancelTransaction(debit.Id, Guid.Empty);
            Console.WriteLine(debit.ToString());

            tinkoffBank.Withdraw(debit.Id, 30000);
            Console.WriteLine(debit.ToString());
        }
    }
}
