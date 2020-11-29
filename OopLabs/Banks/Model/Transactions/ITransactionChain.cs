using System;

namespace Banks.Model.Transactions
{
    public interface ITransactionChain
    {
        ITransactionChain SetNext(ITransactionChain transactionChain);
        public void Put(Decimal sum);
        public void Withdraw(Decimal sum);
        public void Transfer(Decimal sum);
        public void Undo();
    }
}