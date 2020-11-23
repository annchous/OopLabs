using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Banks.Model.Observer
{
    public class InterestTimer : IObservable
    {
        private Task _task;
        private decimal _sumPerMonth;
        private readonly decimal _currentBalance;
        private readonly double _interestOnBalance;
        private readonly List<IObserver> _observers;
        private readonly CancellationTokenSource _cancelTokenSource;
        private readonly CancellationToken _token;
        public readonly DateTimeProvider CurrentDate = new DateTimeProvider();

        public InterestTimer(ref decimal currentBalance, ref double interestOnBalance)
        {
            _currentBalance = currentBalance;
            _interestOnBalance = interestOnBalance;
            _sumPerMonth = 0;
            _observers = new List<IObserver>();
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
            CurrentDate.Now = DateTime.Now;
        }

        public void RegisterObserver(IObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IObserver observer)
        {
            _cancelTokenSource.Cancel();
            _observers.Remove(observer);
        }
        public void NotifyObservers() => _observers.ForEach(observer => observer.UpdateBalance(_sumPerMonth));
        public void UpdateBalance()
        {

            _task = new Task(delegate
            {
                while (true)
                {
                    int i = 0;
                    if (_token.IsCancellationRequested) return;
                    while (i < 30)
                    {
                        if (DateTime.Now.AddDays(1) != CurrentDate.Now.AddDays(1)) continue;
                        _sumPerMonth += _currentBalance * (decimal)(_interestOnBalance / 365) / 100;
                        CurrentDate.Now = DateTime.Now;
                        i++;
                    }
                    NotifyObservers();
                }
            });
            _task.Start();
        }
    }
}