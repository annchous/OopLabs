using System;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    class DepositTransfer : Transaction
    {
        public DepositTransfer(Account sourceAccount, Account destinationAccount) : base(sourceAccount, destinationAccount) {}

        public override void Transfer(decimal sum)
        {
            if (SourceAccount is DepositAccount depositAccount)
            {
                if (depositAccount.Time.Milliseconds > 0) throw new DepositTermNotExpiredException();
                if (sum > depositAccount.Balance) throw new InsufficientFundsException();

                SourceAccount.CareTracker.Backup();
                DestinationAccount.CareTracker.Backup();

                SourceAccount.Balance -= sum;
                DestinationAccount.Balance += sum;
            }
            else TransactionChain?.Transfer(sum);
        }
    }
}
