namespace Banks.Model
{
    public class BalanceMemento : IMemento
    {
        private readonly decimal _balanceState;

        public BalanceMemento(decimal balanceState)
        {
            _balanceState = balanceState;
        }

        public decimal GetState()
        {
            return this._balanceState;
        }
    }
}