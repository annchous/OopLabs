using System;

namespace Banks.Model.Memento
{
    public class BalanceMemento : IMemento
    {
        private readonly Decimal _balanceState;
        public BalanceMemento(Decimal balanceState) => _balanceState = balanceState;
        public Decimal GetState() => _balanceState;
    }
}