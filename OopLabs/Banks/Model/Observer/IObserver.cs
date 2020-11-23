namespace Banks.Model.Observer
{
    public interface IObserver
    {
        public void UpdateBalance(object obj);
        public void UpdateTerm(object obj);
    }
}