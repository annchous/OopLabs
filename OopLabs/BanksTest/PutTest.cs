using System;
using Banks.Model;
using Banks.Model.Accounts;
using NUnit.Framework;

namespace BanksTest
{
    public class PutTest
    {
        private Bank bank;
        private Client client1;
        private DebitAccount debitAccount1;

        [SetUp]
        public void Setup()
        {
            bank = new Bank("Tinkoff Bank");
            client1 = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9");
            debitAccount1 = new DebitAccount(client1, 10000, 2);
            bank.AddClient(client1);
            bank.AddAccountToClient(client1, debitAccount1);
        }

        [Test]
        public void Put()
        {
            bank.Put(debitAccount1.Id, 14000);
            Assert.AreEqual(24000, debitAccount1.Balance);
        }

        [Test]
        public void CancelPut()
        {
            bank.CancelTransaction(debitAccount1.Id, Guid.Empty);
            Assert.AreEqual(10000, debitAccount1.Balance);
        }
    }
}