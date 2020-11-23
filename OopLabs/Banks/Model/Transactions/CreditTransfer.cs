using System;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class CreditTransfer : Transaction
    {
        public CreditTransfer(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}
        public override void Transfer(decimal sum)
        {

            if (SourceAccount is CreditAccount creditAccount)
            {
                if (creditAccount.Balance - sum < -creditAccount.Limit) throw new CreditLimitExceededException();

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Transfer(sum);
        }
    }
}