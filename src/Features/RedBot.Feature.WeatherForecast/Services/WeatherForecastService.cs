using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBot.Feature.WeatherForecast.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public string GetForecast(string cityName)
        {
            return "test string";
        }
    }
}
