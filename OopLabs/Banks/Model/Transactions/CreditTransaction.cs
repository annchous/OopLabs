using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class CreditTransaction : Transaction
    {
        public CreditTransaction(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}

        public override void Execute(decimal sum)
        {

            if (SourceAccount is CreditAccount creditAccount)
            {
                if (creditAccount.Balance - sum < -creditAccount.Limit) throw new Exception("Недостаточно средств. Лимит превышен.");

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Execute(sum);
        }
    }
}