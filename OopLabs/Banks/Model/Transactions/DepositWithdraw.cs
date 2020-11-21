using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class DepositWithdraw : Transaction
    {
        public DepositWithdraw(Account sourceAccount, Account destinationAccount = null) : base(sourceAccount, destinationAccount) {}

        public override void Withdraw(decimal sum)
        {
            if (SourceAccount is DepositAccount depositAccount)
            {
                if (depositAccount.Time > TimeSpan.Zero) throw new Exception("Срок депозита ещё не истёк");
                SourceAccount.CareTracker.Backup();
                SourceAccount.Balance -= sum;
            }
            else TransactionChain?.Withdraw(sum);
        }
    }
}