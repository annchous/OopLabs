using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    class DepositTransaction : Transaction
    {
        public DepositTransaction(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}

        public override void Execute(decimal sum)
        {
            if (SourceAccount is DepositAccount depositAccount)
            {
                if (depositAccount.Time.Milliseconds > 0) throw new Exception("Срок депозита ещё не истёк");

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Execute(sum);
        }
    }
}
