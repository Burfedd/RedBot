using System.Net;

namespace RedBot.Feature.WeatherForecast.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public string GetForecast(string cityName)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.User)}").Result;
                string response = result.Content.ReadAsStringAsync().Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return ProcessWeatherResponse(response);
                }
                else
                {
                    return "Something went wrong";
                }
            }
        }

        private string ProcessWeatherResponse(string jsonString)
        {
            return jsonString;
        }
    }
}
