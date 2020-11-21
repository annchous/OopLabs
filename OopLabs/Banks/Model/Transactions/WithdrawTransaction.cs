using System;
using System.Runtime.Serialization.Formatters;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(Account sourceAccount, Account destinationAccount = null) : base(sourceAccount, destinationAccount) {}

        public override void Withdraw(decimal sum)
        {
            if (sum > SourceAccount.Balance)
            {
                if (SourceAccount is CreditAccount creditAccount)
                    if (creditAccount.Balance - sum < -creditAccount.Limit) throw new Exception("Превышен лимит");
                else throw new Exception("Недостаточно средств");
            }
            else if (SourceAccount is DepositAccount depositAccount)
                if (depositAccount.Time > TimeSpan.Zero) throw new Exception("Срок депозита ещё не истёк");
            
            SourceAccount.CareTracker.Backup();
            SourceAccount.Balance -= sum;
        }
    }
}