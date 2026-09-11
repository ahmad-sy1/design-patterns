using ObserverPattern.Interfaces;

namespace ObserverPattern.Displays
{
    internal class StatisticsDisplay : Observer, DisplayElement
    {
        private float temperature;
        private float sumTemperature = 0;
        private float maxTemp = 0;
        private float minTemp = 0;
        private int countUpdated = 0;
        private Subject weatherData;

        public StatisticsDisplay(Subject weatherData)
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            temperature = temp;
            sumTemperature += temp;
            countUpdated++;

            if (countUpdated == 1 || temp > maxTemp)
            {
                maxTemp = temp;
            }
            if (countUpdated == 1 || temp < minTemp)
            {
                minTemp = temp;
            }

            Display();
        }

        public void Display()
        {
            float average = sumTemperature / countUpdated;
            Console.WriteLine($"Statistieken: gemiddelde={average}°C, max={maxTemp}°C, min={minTemp}°C");
        }
    }
}
