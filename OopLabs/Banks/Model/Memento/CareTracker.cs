using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Exceptions;
using Banks.Model.Accounts;

namespace Banks.Model.Memento
{
    public class CareTracker
    {
        private readonly Account _account;
        private readonly List<IMemento> _mementos = new List<IMemento>();

        public CareTracker(Account account) => _account = account;
        public void Backup() => _mementos.Add(this._account.Save());
        public void Undo()
        {
            if (_mementos.Count == 0) return;

            var memento = _mementos.Last();
            _mementos.Remove(memento);

            try
            {
                _account.Restore(memento);
            }
            catch (UnknownMementoClassException)
            {
                Undo();
            }
        }
    }
}