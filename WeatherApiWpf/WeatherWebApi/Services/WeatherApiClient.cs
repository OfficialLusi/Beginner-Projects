namespace WeatherWebApi.Services;

public class WeatherApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherApiClient> logger) : IWeatherApiClient
{
    #region private fields
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<WeatherApiClient> _logger = logger;
    #endregion

    #region public methods
    
    public async Task<string> GetWeatherDataAsync(string city, CancellationToken cancellationToken = default)
    {
        string baseUrl = _configuration["WeatherAPI:BaseUrl"];
        string apiKey = _configuration["WeatherAPI:ApiKey"];

        if (string.IsNullOrEmpty(baseUrl))
        {
            _logger.LogError("WeatherApiClient - baseUrl cannot be null");
            throw new HttpRequestException("Base URL is null");
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogError("WeatherApiClient - apiKey cannot be null");
            throw new HttpRequestException("API key is null");
        }

        string endPoint = $"{baseUrl}/{city}?key={apiKey}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endPoint, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "WeatherApiClient - Error during API call");
            throw;
        }
    }

    #endregion
}