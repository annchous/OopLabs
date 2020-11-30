using System;

namespace Banks.Model.Memento
{
    public interface IMemento
    {
        public Decimal GetState();
    }
}