using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class Transaction : ITransactionChain
    {
        protected readonly Account SourceAccount;
        protected readonly Account DestinationAccount;
        protected ITransactionChain TransactionChain { get; private set; }
        public Transaction(Account sourceAccount, Account destinationAccount = null)
        {
            SourceAccount = sourceAccount;
            DestinationAccount = destinationAccount;
        }

        public ITransactionChain SetNext(ITransactionChain transactionChain)
        {
            TransactionChain = transactionChain;
            return transactionChain;
        }

        public virtual void Put(decimal sum)
        {
            TransactionChain = new PutTransaction(SourceAccount);
            TransactionChain.Put(sum);
        }

        public virtual void Withdraw(decimal sum)
        {
            TransactionChain = 
                    new DebitWithdraw(SourceAccount)
                    .SetNext(new DepositWithdraw(SourceAccount))
                    .SetNext(new CreditWithdraw(SourceAccount));
            TransactionChain.Withdraw(sum);
        }

        public virtual void Execute(decimal sum)
        {
            TransactionChain = 
                        new DebitTransaction(SourceAccount, DestinationAccount)
                        .SetNext(new DepositTransaction(SourceAccount, DestinationAccount))
                        .SetNext(new CreditTransaction(SourceAccount, DestinationAccount));
            TransactionChain.Execute(sum);
        }
        
        public void Undo()
        {
            SourceAccount.CareTracker.Undo();
            DestinationAccount?.CareTracker.Undo();
        }
    }
}