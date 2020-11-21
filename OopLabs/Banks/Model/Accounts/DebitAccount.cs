using System;

namespace Banks.Model.Accounts
{
    sealed class DebitAccount : Account
    {
        public DebitAccount(Client accountOwner, decimal balance, double interestOnBalance) : base(accountOwner, balance)
        {
            InterestOnBalance = interestOnBalance;
        }

        public override double InterestOnBalance { get; }
        public override void Put(decimal sum)
        {
            if (sum <= 0) throw new Exception("Счёт можно пополнить только на положительную сумму");
            Balance += sum;
        }

        public override void Withdraw(decimal sum)
        {
            if (sum > Balance) throw new Exception("Недостаточно средств");
            Balance -= sum;
        }
    }
}
