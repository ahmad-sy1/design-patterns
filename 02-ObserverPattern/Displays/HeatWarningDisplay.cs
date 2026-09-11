using ObserverPattern.Interfaces;

namespace ObserverPattern.Displays
{
    internal class HeatWarningDisplay : Observer, DisplayElement
    {
        private float temperature;
        private Subject weatherData;

        public HeatWarningDisplay(Subject weatherData)
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            this.temperature = temp;
            Display();
        }

        public void Display()
        {
            if (temperature > 30)
            {
                Console.WriteLine("WAARSCHUWING: extreme hitte!");
            }
            else
            {
                Console.WriteLine("Geen hittewaarschuwing.");
            }
        }
    }
}
