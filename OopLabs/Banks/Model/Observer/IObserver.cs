using System;

namespace Banks.Model.Observer
{
    public interface IObserver
    {
        public void UpdateBalance(decimal sum);
        public void UpdateTerm(TimeSpan term);
    }
}