using Microsoft.AspNetCore.Mvc;
using WeatherWebApi.DTO.TreatedDto;
using WeatherWebApi.Services;

namespace WeatherWebApi.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController(IWeatherApiService service) : ControllerBase
{
    #region private fields
    private readonly IWeatherApiService _service = service;
    #endregion

    #region public methods

    /// <summary>
    /// https://localhost:7298/api/weather/{cityName}
    /// </summary>
    /// <param name="city"></param>
    /// <returns>http respons with serialized json of type ToClientWeatherDataResponse</returns>
    [HttpGet("{city}/{date}")]
    public async Task<IActionResult> GetWeatherData(string city, string date, CancellationToken cancellationToken)
    {
        try
        {
            ToClientWeatherDataResponse response = await _service.GetData(city, date, cancellationToken);
            return Ok(response);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Operation canceled by client");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected error: {ex.Message}");
        }
    }

    #endregion
}
