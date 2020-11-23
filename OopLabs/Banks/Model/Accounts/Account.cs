using System;
using System.Text;
using Banks.Exceptions;
using Banks.Model.Memento;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public abstract class Account : IObserver
    {
        protected Guid _id;
        protected decimal _balance;
        protected double _interestOnBalance;
        public Guid Id => _id;
        public virtual decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }
        public Client.Client AccountOwner { get; }
        public CareTracker CareTracker { get; }
        public InterestTimer InterestTimer { get; }
        public abstract double InterestOnBalance { get; set; }

        protected Account(Client.Client accountOwner, decimal balance, double interestOnBalance = 0)
        {
            _id = Guid.NewGuid();
            AccountOwner = accountOwner;
            _balance = balance;
            _interestOnBalance = interestOnBalance;
            CareTracker = new CareTracker(this);
            InterestTimer = new InterestTimer(ref _balance, ref _interestOnBalance);
            InterestTimer.RegisterObserver(this);
            InterestTimer.UpdateBalance();
        }

        public IMemento Save() => new BalanceMemento(Balance);
        public void Restore(IMemento memento)
        {
            if (memento is not BalanceMemento)
                throw new UnknownMementoClassException(memento.ToString());
            Balance = memento.GetState();
        }

        public override string ToString() => 
            new StringBuilder()
                .AppendLine($"{this.GetType().Name}")
                .AppendLine($"Id: {Id}")
                .AppendLine($"Balance: {Balance}")
                .ToString();

        public void UpdateBalance(object obj) => Balance += (decimal)obj;
        public virtual void UpdateTerm(object obj) => throw new NotImplementedException();
        public void StopUpgrade() => InterestTimer.RemoveObserver(this);
    }
}