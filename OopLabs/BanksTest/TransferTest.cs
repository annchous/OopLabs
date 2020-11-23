using System;
using Banks.Model;
using NUnit.Framework;
using Banks.Model.Accounts;
using Banks.Exceptions;
using Banks.Model.Bank;
using Banks.Model.Client;

namespace BanksTest
{
    public class TransferTest
    {
        private Bank _bank;
        private Client _client1;
        private Client _client2;
        private DebitAccount _debitAccount1;
        private DebitAccount _debitAccount2;

        [SetUp]
        public void Setup()
        {
            _bank = new Bank("Tinkoff Bank");
            _client1 = Client.Builder("Fredi", "Kats").SetAddress("Ћесной пр-кт, д. 9");
            _client2 = Client.Builder("Vlad", "Kazanskiy").SetAddress("¬€земский переулок, 5-7").SetPassport("1234567890");
            _debitAccount1 = new DebitAccount(_client1, 10000, 2);
            _debitAccount2 = new DebitAccount(_client2, 13000, 2);
            _bank.AddClient(_client1);
            _bank.AddClient(_client2);
            _bank.AddAccountToClient(_client1, _debitAccount1);
            _bank.AddAccountToClient(_client2, _debitAccount2);
        }

        [Test]
        public void DebitTransfer_Result()
        {
            _bank.Transfer(_debitAccount2.Id, _debitAccount1.Id, 2000);
            Assert.AreEqual(12000, _debitAccount1.Balance);
            Assert.AreEqual(11000, _debitAccount2.Balance);
        }

        [Test]
        public void DebitTransfer_Exception()
        {
            Assert.Throws<NotVerifiedClientException>(() => _bank.Transfer(_debitAccount1.Id, _debitAccount2.Id, 2000));
        }

        [Test]
        public void CancelTransfer()
        {
            _bank.CancelTransaction(_debitAccount1.Id, _debitAccount2.Id);
            Assert.AreEqual(10000, _debitAccount1.Balance);
            Assert.AreEqual(13000, _debitAccount2.Balance);
        }
    }
}