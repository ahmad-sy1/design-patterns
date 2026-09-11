using ObserverPattern.Displays;

namespace ObserverPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionDisplay currentConditionDisplay = new CurrentConditionDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
            HeatWarningDisplay heatWarningDisplay = new HeatWarningDisplay(weatherData);

            weatherData.SetMeasurements(28, 65, 30.4f);
            weatherData.SetMeasurements(29, 70, 29.2f);
            weatherData.SetMeasurements(32, 90, 29.2f);

            Console.WriteLine("--- ForecastDisplay unsubscribed ---");
            weatherData.RemoveObserver(forecastDisplay);

            weatherData.SetMeasurements(20, 40, 29.2f);
        }
    }
}
