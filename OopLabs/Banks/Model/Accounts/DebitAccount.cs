using System;

namespace Banks.Model.Accounts
{
    sealed class DebitAccount : Account
    {
        public DebitAccount(Client accountOwner, decimal balance, double interestOnBalance) : base(accountOwner, balance)
        {
            InterestOnBalance = interestOnBalance;
        }

        public override double InterestOnBalance { get; }
    }
}
