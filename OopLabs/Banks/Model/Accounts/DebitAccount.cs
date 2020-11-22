using System;

namespace Banks.Model.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client accountOwner, decimal balance, double interestOnBalance) : base(accountOwner, balance)
        {
            InterestOnBalance = interestOnBalance;
        }

        public override double InterestOnBalance { get; }
    }
}
