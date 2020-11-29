using System;
using Banks.Model.Clients;

namespace Banks.Model.Accounts
{
    public class CreditAccount : Account
    {
        public Double Fee { get; }
        public Decimal Limit { get; }

        public CreditAccount(Client accountOwner, Decimal balance, Decimal limit, Double fee) 
            : base(accountOwner, balance)
        {
            Limit = limit;
            Fee = fee;
        }

        public override Decimal Balance 
        {
            get => balance < 0
                ? balance - (Decimal) (Fee / 1000) * Math.Abs(balance)
                : balance;
            set => balance = value;
        }
    }
}