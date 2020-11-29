using System;

namespace Banks.Model.Observer
{
    public interface ITermObserver
    {
        public void UpdateTerm(TimeSpan term);
    }
}