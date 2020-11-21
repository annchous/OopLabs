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
        public override void Put(decimal sum)
        {
            throw new System.NotImplementedException();
        }

        public override void Withdraw(decimal sum)
        {
            if (Balance - sum < -Limit) throw new Exception("Превышен лимит");
            Balance -= sum;
        }
    }
}