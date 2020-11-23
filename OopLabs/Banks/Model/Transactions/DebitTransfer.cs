using System;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class DebitTransfer : Transaction
    {
        public DebitTransfer(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}
        public override void Transfer(decimal sum)
        {
            if (SourceAccount is DebitAccount debitAccount)
            {
                if (sum > debitAccount.Balance) throw new InsufficientFundsException();

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Transfer(sum);
        }
    }
}