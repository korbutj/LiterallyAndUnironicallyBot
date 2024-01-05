using Bot.Data;
using Bot.Services.Weather.Models;

namespace Bot.Services.Weather;

public class WeatherService : IWeatherService
{
    private readonly MultiWeatherApi.IWeatherService weatherService;
    private readonly IRepository repository;

    public WeatherService(MultiWeatherApi.IWeatherService weatherService, IRepository repository)
    {
        this.weatherService = weatherService;
        this.repository = repository;
    }
    
    public Task<WeatherForecast> GetWeatherForecast(string location)
    {
        throw new NotImplementedException();
    }
}