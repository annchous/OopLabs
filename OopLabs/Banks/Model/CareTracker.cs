using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Model.Accounts;

namespace Banks.Model
{
    public class CareTracker
    {
        private readonly List<IMemento> _mementos = new List<IMemento>();
        private readonly Account _account;

        public CareTracker(Account account)
        {
            this._account = account;
        }

        public void Backup()
        {
            this._mementos.Add(this._account.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            try
            {
                this._account.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }
    }
}