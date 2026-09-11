namespace ObserverPattern.Interfaces
{
    internal interface Observer
    {
        void Update(float temp, float humidity, float pressure);
    }
}
