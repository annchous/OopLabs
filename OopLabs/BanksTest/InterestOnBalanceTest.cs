using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Banks.Model;
using Banks.Model.Accounts;
using Banks.Model.Bank;
using Banks.Model.Client;
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
            _client1 = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9");
            _debitAccount1 = new DebitAccount(_client1, 10000, 2);
            _bank.AddClient(_client1);
            _bank.AddAccountToClient(_client1, _debitAccount1);
        }

        [Test]
        public void Test()
        {
            _debitAccount1.InterestTimer.CurrentDate.Now = DateTime.Now.AddDays(31);
            _debitAccount1.StopUpgrade();
            Assert.AreNotSame(10000, _debitAccount1.Balance);
        }
    }
}
