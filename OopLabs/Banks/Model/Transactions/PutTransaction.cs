using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class PutTransaction : Transaction
    {
        public PutTransaction(Account sourceAccount, Account destinationAccount = null) : base(sourceAccount, destinationAccount) {}
        public override void Put(decimal sum)
        {
            SourceAccount.CareTracker.Backup();
            SourceAccount.Balance += sum;
        }
    }
}