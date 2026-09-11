using ObserverPattern.Interfaces;

namespace ObserverPattern.Displays
{
    internal class ForecastDisplay : Observer, DisplayElement
    {
        private float temperature;
        private float humidity;
        private Subject weatherData;

        public ForecastDisplay(Subject weatherData)
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            this.temperature = temp;
            this.humidity = humidity;
            Display();
        }

        public void Display()
        {
            if (humidity > 80 || temperature < 20)
            {
                Console.WriteLine("Voorspelling: Trek je paraplu's uit de kast");
            }
            else
            {
                Console.WriteLine("Voorspelling: Mooi weer komt er aan");
            }
        }
    }
}
