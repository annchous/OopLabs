using System;
using Banks.Model.Accounts;
using Banks.Model.Banks;
using Banks.Model.Clients;
using NUnit.Framework;

namespace BanksTest
{
    public class InterestOnBalanceTest
    {
        private Bank _bank;
        private Client _client1;
        private DebitAccount _debitAccount1;
        
        [SetUp]
        public void Setup()
        {
            _bank = new Bank("Tinkoff Bank");
            _client1 = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9").GetClient();
            _debitAccount1 = new DebitAccount(_client1, 10000, 2);
            _bank.AddClient(_client1);
            _bank.AddAccountToClient(_client1, _debitAccount1);
        }

        [Test]
        public void Test()
        {
            _debitAccount1.InterestTimer.CurrentDate.Now = DateTime.Now.AddDays(31);
            _debitAccount1.StopUpgrade();
            Assert.AreNotSame(10000m, _debitAccount1.Balance);
        }
    }
}
