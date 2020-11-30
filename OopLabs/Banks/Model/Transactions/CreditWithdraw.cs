using System;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class CreditWithdraw : Transaction
    {
        public CreditWithdraw(Account sourceAccount, Account destinationAccount = null) 
            : base(sourceAccount, destinationAccount) 
        {}

        public override void Withdraw(Decimal sum)
        {
            if (SourceAccount is CreditAccount creditAccount)
            {
                if (creditAccount.Balance - sum < -creditAccount.Limit) throw new CreditLimitExceededException();
                SourceAccount.CareTracker.Backup();
                SourceAccount.Balance -= sum;
            }
            else TransactionChain?.Withdraw(sum);
        }
    }
}