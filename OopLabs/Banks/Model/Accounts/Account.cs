using System;
using System.Text;

namespace Banks.Model.Accounts
{
    public abstract class Account
    {
        public Guid Id { get; }
        public decimal Balance { get; set; }
        public abstract double InterestOnBalance { get; }
        public CareTracker CareTracker { get; }
        public Client AccountOwner { get; set; }

        protected Account(Client accountOwner, decimal balance)
        {
            Id = Guid.NewGuid();
            AccountOwner = accountOwner;
            Balance = balance;
            CareTracker = new CareTracker(this);
        }

        public IMemento Save() => new BalanceMemento(Balance);

        public void Restore(IMemento memento)
        {
            if (!(memento is BalanceMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }
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