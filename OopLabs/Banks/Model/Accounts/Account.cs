using System;
using System.Text;
using Banks.Exceptions;
using Banks.Model.Clients;
using Banks.Model.Memento;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public abstract class Account
    {
        protected Decimal balance;
        public Guid Id { get; }
        public Client AccountOwner { get; }
        public CareTracker CareTracker { get; }
        public virtual Decimal Balance { get; set; }
        public Decimal CurrentInterest { get; set; }
        public DateTime LastInterestCharge { get; set; }
        public virtual Double InterestOnBalance { get; }

        protected Account(Client accountOwner, Decimal balance, Double interestOnBalance = 0)
        {
            Id = Guid.NewGuid();
            this.balance = balance;
            AccountOwner = accountOwner;
            InterestOnBalance = interestOnBalance;
            LastInterestCharge = DateTime.Now;
            CareTracker = new CareTracker(this);
        }

        public void ChargeDailyInterest(Int32 days, DateTime updateTime)
        {
            CurrentInterest += Balance * days * (Decimal)(InterestOnBalance / 365) / 100;
            LastInterestCharge = updateTime;
        }

        public void UpdateBalance(Decimal sum)
        {
            Balance += sum;
        }

        public IMemento Save()
        {
            return new BalanceMemento(Balance);
        }

        public void Restore(IMemento memento)
        {
            if (memento is not BalanceMemento)
                throw new UnknownMementoClassException(memento.ToString());
            Balance = memento.GetState();
        }

        public override String ToString() => new StringBuilder()
                .AppendLine($"{this.GetType().Name}")
                .AppendLine($"Id: {Id}")
                .AppendLine($"Balance: {Balance}")
                .ToString();
    }
}