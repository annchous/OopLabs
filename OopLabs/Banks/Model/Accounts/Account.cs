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
        protected Guid _id;
        protected decimal _balance;
        public Guid Id => _id;
        public virtual decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }
        public Client AccountOwner { get; }
        public CareTracker CareTracker { get; }
        public InterestTimer InterestTimer { get; protected set; }

        protected Account(Client accountOwner, decimal balance)
        {
            _id = Guid.NewGuid();
            AccountOwner = accountOwner;
            _balance = balance;
            CareTracker = new CareTracker(this);
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
    }
}