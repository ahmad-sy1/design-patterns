using ObserverPattern.Interfaces;

namespace ObserverPattern
{
    internal class WeatherData : Subject
    {
        private List<Observer> observers;
        private float temperature;
        private float humidity;
        private float pressure;

        public WeatherData()
        {
            observers = new List<Observer>();
        }

        public void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.Update(temperature, humidity, pressure);
            }
        }

        public void RegisterObserver(Observer o)
        {
            if (!observers.Contains(o))
            {
                observers.Add(o);
            }
        }

        public void RemoveObserver(Observer o)
        {
            if (observers.Contains(o))
            {
                observers.Remove(o);
            }
        }

        public void MeasurementChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float tempereature, float humidity, float pressure)
        {
            this.temperature = tempereature;
            this.humidity = humidity;
            this.pressure = pressure;
            MeasurementChanged();
        }
    }
}
