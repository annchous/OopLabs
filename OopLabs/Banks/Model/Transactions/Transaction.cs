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

        public virtual void Put(Decimal sum)
        {
            TransactionChain = new PutTransaction(SourceAccount);
            TransactionChain.Put(sum);
        }

        public virtual void Withdraw(Decimal sum)
        {
            TransactionChain = new DebitWithdraw(SourceAccount);
            TransactionChain
                .SetNext(new DepositWithdraw(SourceAccount))
                .SetNext(new CreditWithdraw(SourceAccount));
            TransactionChain.Withdraw(sum);
        }

        public virtual void Transfer(Decimal sum)
        {
            TransactionChain = new DebitTransfer(SourceAccount, DestinationAccount);
            TransactionChain
                .SetNext(new DepositTransfer(SourceAccount, DestinationAccount))
                .SetNext(new CreditTransfer(SourceAccount, DestinationAccount));
            TransactionChain.Transfer(sum);
        }
        
        public void Undo()
        {
            SourceAccount.CareTracker.Undo();
            DestinationAccount?.CareTracker.Undo();
        }
    }
}