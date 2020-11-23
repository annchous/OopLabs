using System;
using Banks.Model.Clients;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public class DebitAccount : Account, IObserver
    {
        public DebitAccount(Client accountOwner, decimal balance, double interestOnBalance) : base(accountOwner, balance)
        {
            InterestOnBalance = interestOnBalance;
            InterestTimer = new InterestTimer(this, InterestOnBalance);
            InterestTimer.RegisterObserver(this);
            InterestTimer.UpdateBalance();
        }
        public double InterestOnBalance { get; }
        public void StopUpgrade() => InterestTimer.RemoveObserver(this);
        public void UpdateBalance(decimal sum) => Balance += sum;
        public void UpdateTerm(TimeSpan term) => throw new NotImplementedException();
    }
}
