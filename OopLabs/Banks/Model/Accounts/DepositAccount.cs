using System;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public class DepositAccount : Account
    {
        public TimeSpan Term { get; set; }
        public TermTimer TermTimer { get; }
        public override double InterestOnBalance
        {
            get =>
                _balance switch
                {
                    < 50000 => 3,
                    < 100000 => 3.5,
                    >= 10000 => 4
                };
            set => _interestOnBalance = value;
        }

        public DepositAccount(Client.Client accountOwner, decimal balance, TimeSpan term) : base(accountOwner, balance)
        {
            Term = term;
            TermTimer = new TermTimer(Term);
            TermTimer.RegisterObserver(this);
            TermTimer.UpdateTerm();
        }

        public override void UpdateTerm(object obj) => Term = (TimeSpan) obj;
    }
}