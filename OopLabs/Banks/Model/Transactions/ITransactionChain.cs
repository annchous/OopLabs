namespace Banks.Model.Transactions
{
    public interface ITransactionChain
    {
        ITransactionChain SetNext(ITransactionChain transactionChain);
        public void Put(decimal sum);
        public void Withdraw(decimal sum);
        public void Transfer(decimal sum);
        public void Undo();
    }
}