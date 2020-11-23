using System;
using Banks.Model.Clients;

namespace Banks.Model.Accounts
{
    public class CreditAccount : Account
    {
        public decimal Limit { get; }
        public double Fee { get; }
        public CreditAccount(Client accountOwner, decimal balance, decimal limit, double fee) : base(accountOwner, balance)
        {
            Limit = limit;
            Fee = fee;
        }
        public override decimal Balance => _balance < 0 ? _balance - (decimal) (Fee / 1000) * Math.Abs(_balance) : _balance;
    }
}