using System;
using Banks.Model.Clients;
using Banks.Model.Observer;

namespace Banks.Model.Accounts
{
    public class DebitAccount : Account
    {
        public override Decimal Balance { get => balance; set => balance = value; }

        public DebitAccount(Client accountOwner, Decimal balance, Double interestOnBalance) 
            : base(accountOwner, balance, interestOnBalance)
        {}
    }
}
