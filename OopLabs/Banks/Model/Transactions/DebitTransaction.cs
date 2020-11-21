using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class DebitTransaction : Transaction
    {
        public DebitTransaction(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}

        public override void Execute(decimal sum)
        {
            if (SourceAccount is DebitAccount debitAccount)
            {
                if (sum > debitAccount.Balance) throw new Exception("Недостаточно средств");

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Execute(sum);
        }
    }
}