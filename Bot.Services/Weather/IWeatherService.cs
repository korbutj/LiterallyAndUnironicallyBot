using Bot.Services.Weather.Models;

namespace Bot.Services.Weather;

public interface IWeatherService
{
    Task<WeatherForecast> GetWeatherForecast(string location);
}