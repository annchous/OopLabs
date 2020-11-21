using System;

namespace Banks.Model.Accounts
{
    public class CreditAccount : Account
    {
        public decimal Limit { get; }

        public double Fee { get; set; }

        public CreditAccount(Client accountOwner, decimal balance, decimal limit, double fee) : base(accountOwner, balance)
        {
            Limit = limit;
            Fee = fee;
        }

        public override double InterestOnBalance { get; } = 0;
    }
}