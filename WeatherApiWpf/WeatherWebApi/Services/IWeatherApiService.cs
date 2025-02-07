using WeatherWebApi.DTO.ApiDto;
using WeatherWebApi.DTO.TreatedDto;

namespace WeatherWebApi.Services;

public interface IWeatherApiService
{
    public Task<ToClientWeatherDataResponse> GetData(string cityName, string date, CancellationToken cancellationToken);
}
