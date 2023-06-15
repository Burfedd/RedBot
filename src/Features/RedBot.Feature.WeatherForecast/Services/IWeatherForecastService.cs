namespace RedBot.Feature.WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        string GetForecast(string cityName);
    }
}
