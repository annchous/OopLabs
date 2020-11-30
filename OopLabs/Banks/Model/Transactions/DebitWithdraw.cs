using System;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class DebitWithdraw : Transaction
    {
        public DebitWithdraw(Account sourceAccount, Account destinationAccount = null) 
            : base(sourceAccount, destinationAccount) 
        {}

        public override void Withdraw(Decimal sum)
        {
            if (SourceAccount is DebitAccount debitAccount)
            {
                if (sum > debitAccount.Balance) 
                    throw new InsufficientFundsException();

                SourceAccount.CareTracker.Backup();
                SourceAccount.Balance -= sum;
            }
            else TransactionChain?.Withdraw(sum);
        }
    }
}