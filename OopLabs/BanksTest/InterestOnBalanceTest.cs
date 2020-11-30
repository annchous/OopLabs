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
        private Client _client;
        private DebitAccount _debitAccount;
        
        [SetUp]
        public void Setup()
        {
            _bank = new Bank("Tinkoff Bank");
            _client = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9").SetPassport("1111223344").GetClient();
            _debitAccount = new DebitAccount(_client, 10000, 2);
            _bank.AddClient(_client);
            _bank.AddAccountToClient(_client, _debitAccount);
        }

        [Test]
        public void Test()
        {
            _bank.BankTime.CurrentDate = DateTime.Now.AddDays(1);
            _bank.Put(_debitAccount.Id, 100);
            _bank.BankTime.CurrentDate = _bank.BankTime.CurrentDate.AddDays(16);
            _bank.Withdraw(_debitAccount.Id, 100);
            _bank.UpdateAccountBalance();
            Assert.AreNotEqual(10000, _debitAccount.Balance);
        }
    }
}
