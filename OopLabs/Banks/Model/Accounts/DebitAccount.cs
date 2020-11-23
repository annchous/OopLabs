using System;

namespace Banks.Model.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client.Client accountOwner, decimal balance, double interestOnBalance) : base(accountOwner, balance, interestOnBalance) {}

        public override double InterestOnBalance
        {
            get => _interestOnBalance;
            set => _interestOnBalance = value;
        }
    }
}
