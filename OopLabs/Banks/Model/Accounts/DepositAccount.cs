using System;
using Banks.Model.Clients;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public class DepositAccount : Account, IObserver
    {
        public TimeSpan Term { get; set; }
        public TermTimer TermTimer { get; }
        public double InterestOnBalance => _balance switch
            {
                < 50000 => 3,
                < 100000 => 3.5,
                >= 10000 => 4
            };

        public DepositAccount(Client accountOwner, decimal balance, TimeSpan term) : base(accountOwner, balance)
        {
            Term = term;
            InterestTimer = new InterestTimer(this, InterestOnBalance);
            InterestTimer.RegisterObserver(this);
            InterestTimer.UpdateBalance();
            TermTimer = new TermTimer(Term);
            TermTimer.RegisterObserver(this);
            TermTimer.UpdateTerm();
        }

        public void UpdateBalance(decimal sum) => Balance += sum;
        public void UpdateTerm(TimeSpan term) => Term = term;
        public void StopUpgrade() => InterestTimer.RemoveObserver(this);
    }
}