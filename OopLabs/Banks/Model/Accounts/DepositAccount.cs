using System;

namespace Banks.Model.Accounts
{
    public class DepositAccount : Account
    {
        public TimeSpan Time { get; set; }
        public override double InterestOnBalance =>
            Balance switch
            {
                < 50000 => 3,
                < 100000 => 3.5,
                >= 10000 => 4
            };
        
        public DepositAccount(Client accountOwner, decimal balance, TimeSpan time) : base(accountOwner, balance)
        {
            Time = time;
        }

        public override void Put(decimal sum)
        {
            if (sum <= 0) throw new Exception("Счёт можно пополнить только на положительную сумму");
            Balance += sum;
        }

        public override void Withdraw(decimal sum)
        {
            if (Time.Milliseconds > 0) throw new Exception("Срок депозита ещё не истёк");
            Balance -= sum;
        }
    }
}