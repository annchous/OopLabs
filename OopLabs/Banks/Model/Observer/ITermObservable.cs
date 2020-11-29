namespace Banks.Model.Observer
{
    public interface ITermObservable
    {
        void RegisterObserver(ITermObserver observer);
        void RemoveObserver(ITermObserver observer);
        void NotifyObservers();
    }
}