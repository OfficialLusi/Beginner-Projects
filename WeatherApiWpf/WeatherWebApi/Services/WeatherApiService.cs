using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using WeatherWebApi.DTO.ApiDto;
using WeatherWebApi.DTO.TreatedDto;
using WeatherWebApi.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherWebApi.Services;

public class WeatherApiService(IWeatherApiClient client/*, IDistributedCache cache*/, ILogger<WeatherApiService> logger, IMapper mapper) : IWeatherApiService
{

    #region private fields
    private readonly IWeatherApiClient _client = client;
    //private readonly IDistributedCache _cache = cache;
    private readonly ILogger<WeatherApiService> _logger = logger;
    private readonly WeatherServicesMapper _utils = new WeatherServicesMapper(mapper);
    //private readonly int _cacheSavingHours = 12;
    #endregion

    #region public methods

    public async Task<ToClientWeatherDataResponse> GetData(string cityName, string date, CancellationToken cancellationToken = default)
    {
        string jsonWeatherResponse = string.Empty;

        //string cacheKey = $"weather:{cityName.ToLower()}_{date}";

        //if (!string.IsNullOrEmpty(jsonWeatherResponse))
        //    return _utils.ConvertFromApiDtoToTreatedDto(DeserializeWeatherData(jsonWeatherResponse), date);

        jsonWeatherResponse = await GetWeatherDataAsync(cityName, cancellationToken);

        if (!string.IsNullOrEmpty(jsonWeatherResponse))
        {
            //await _cache.SetStringAsync(cacheKey, jsonWeatherResponse, SetCacheRetentionsHours(), cancellationToken);
            //_logger.LogInformation("Data cached for key: {CacheKey}", cacheKey);
            return _utils.ConvertFromApiDtoToTreatedDto(DeserializeWeatherData(jsonWeatherResponse), date);
        }

        return null;
    }

    #endregion

    #region private methods

    private async Task<string> GetWeatherDataAsync(string city, CancellationToken cancellationToken)
    {
        try
        {
            return await _client.GetWeatherDataAsync(city, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "WeatherService - Error during processing");
            throw;
        }
    }

    //private async Task<string> GetWeatherDataFromCache(string cacheKey)
    //{
    //    string cachedResponse = await _cache.GetStringAsync(cacheKey);

    //    if (!string.IsNullOrEmpty(cachedResponse))
    //    {
    //        _logger.LogInformation("Data retrieved from cache for key: {cacheKey}", cacheKey);
    //        return cachedResponse;
    //    }
    //    return null;
    //}

    private WeatherResponse DeserializeWeatherData(string jsonData)
    {
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            WeatherResponse response = JsonSerializer.Deserialize<WeatherResponse>(jsonData, options);

            if (response == null)
            {
                _logger.LogError("WeatherService - Failed to deserialize data");
                throw new Exception("Failed to deserialize data");
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "WeatherService - Error during deserialization");
            throw;
        }
    }

    //private DistributedCacheEntryOptions SetCacheRetentionsHours()
    //{
    //    return new DistributedCacheEntryOptions
    //    {
    //        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_cacheSavingHours)
    //    };
    //}
    #endregion
}
