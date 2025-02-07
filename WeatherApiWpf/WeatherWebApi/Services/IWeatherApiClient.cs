namespace WeatherWebApi.Services;

public interface IWeatherApiClient
{
    Task<string> GetWeatherDataAsync(string city, CancellationToken token);
}
