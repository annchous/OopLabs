using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Banks.Model.Observer
{
    public class TermTimer : IObservable
    {
        private Task _task;
        private readonly TimeSpan _totalTime;
        private readonly DateTime _startDate;
        private readonly List<IObserver> _observers;
        private readonly CancellationTokenSource _cancelTokenSource;
        private readonly CancellationToken _token;
        public readonly DateTimeProvider CurrentDate = new DateTimeProvider();

        public TermTimer(TimeSpan totalTime)
        {
            _totalTime = totalTime;
            _startDate = DateTime.Now;
            CurrentDate.Now = DateTime.Now;
            _observers = new List<IObserver>();
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
        }

        public void RegisterObserver(IObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IObserver observer)
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
                    if ((CurrentDate.Now.Date - _startDate.Date).Milliseconds < _totalTime.Milliseconds) continue;
                    break;
                }
                NotifyObservers();
            });
            _task.Start();
        }
    }
}
