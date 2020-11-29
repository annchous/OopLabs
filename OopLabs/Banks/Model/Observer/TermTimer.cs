using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Banks.Model.Observer
{
    public class TermTimer : ITermObservable
    {
        private Task _task;
        private readonly TimeSpan _totalTime;
        private readonly DateTime _startDate;
        private readonly List<ITermObserver> _observers;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _cancelTokenSource;
        public DateTime CurrentDate { get; set; }

        public TermTimer(TimeSpan totalTime)
        {
            _totalTime = totalTime;
            _startDate = DateTime.Now;
            CurrentDate = DateTime.Now;
            _observers = new List<ITermObserver>();
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
        }

        public void RegisterObserver(ITermObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(ITermObserver observer)
        {
            _cancelTokenSource.Cancel();
            _observers.Remove(observer);
        }

        public void NotifyObservers() => _observers.ForEach(observer => observer.UpdateTerm(TimeSpan.Zero));

        public void UpdateTerm()
        {
            _task = new Task(delegate
            {
                while (true)
                {
                    if (_token.IsCancellationRequested) return;
                    if ((CurrentDate.Date - _startDate.Date).Milliseconds < _totalTime.Milliseconds) continue;
                    break;
                }
                NotifyObservers();
            });
            _task.Start();
        }
    }
}
