using System;
using Banks.Model.Clients;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public class DepositAccount : Account, ITermObserver
    {
        public TimeSpan Term { get; private set; }
        public TermTimer TermTimer { get; }
        public override Double InterestOnBalance => balance switch
        {
            < 50000 => 3,
            < 100000 => 3.5,
            >= 10000 => 4
        };
        public override Decimal Balance { get => balance; set => balance = value; }

        public DepositAccount(Client accountOwner, Decimal balance, TimeSpan term) 
            : base(accountOwner, balance)
        {
            Term = term;
            TermTimer = new TermTimer(Term);
            TermTimer.RegisterObserver(this);
            TermTimer.UpdateTerm();
        }

        public void UpdateTerm(TimeSpan term) => Term = term;
        public void StopUpdate() => TermTimer.RemoveObserver(this);
    }
}