using System;
using System.Threading;
using Banks.Exceptions;
using Banks.Model;
using Banks.Model.Accounts;
using Banks.Model.Bank;
using Banks.Model.Client;
using NUnit.Framework;

namespace BanksTest
{
    public class WithdrawTest
    {
        private Bank _bank;
        private Client _client;
        private DebitAccount _debitAccount;
        private DepositAccount _depositAccount;
        private CreditAccount _creditAccount;

        [SetUp]
        public void Setup()
        {
            _bank = new Bank("Tinkoff Bank");
            _client = Client.Builder("Fredi", "Kats").SetAddress("Лесной пр-кт, д. 9").SetPassport("1234567890");
            _debitAccount = new DebitAccount(_client, 10000, 2);
            _depositAccount = new DepositAccount(_client, 5000, TimeSpan.FromSeconds(5));
            _creditAccount = new CreditAccount(_client, 2000, 4000, 2);
            _bank.AddClient(_client);
            _bank.AddAccountToClient(_client, _debitAccount);
            _bank.AddAccountToClient(_client, _depositAccount);
            _bank.AddAccountToClient(_client, _creditAccount);
        }

        [Test]
        public void WithdrawDebit_Result()
        {
            _bank.Withdraw(_debitAccount.Id, 3500);
            Assert.AreEqual(6500, _debitAccount.Balance);
        }

        [Test]
        public void WithdrawDebit_Exception()
        {
            Assert.Throws<InsufficientFundsException>(() => _bank.Withdraw(_debitAccount.Id, 14000));
        }

        [Test]
        public void WithdrawDeposit_Result()
        {
            _depositAccount.TermTimer.CurrentDate.Now = DateTime.Now.AddDays(1);
            _bank.Withdraw(_depositAccount.Id, 3000);
            Assert.AreEqual(2000, _depositAccount.Balance);
        }

        [Test]
        public void WithdrawDeposit_Exception()
        {
            Assert.Throws<DepositTermNotExpiredException>(() => _bank.Withdraw(_depositAccount.Id, 3000));
        }

        [Test]
        public void WithdrawCredit_Result()
        {
            _bank.Withdraw(_creditAccount.Id, 3000);
            Assert.AreEqual(-1002, _creditAccount.Balance);
        }

        [Test]
        public void WithdrawCredit_Exception()
        {
            Assert.Throws<CreditLimitExceededException>(() => _bank.Withdraw(_creditAccount.Id, 10000));
        }
    }
}