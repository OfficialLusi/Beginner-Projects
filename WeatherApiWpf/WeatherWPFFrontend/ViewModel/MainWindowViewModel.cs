using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WeatherWebApi.DTO.TreatedDto;

namespace WeatherWPFFrontend.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _cityName;
    private string _selectDay;
    private string _weatherData;
    private readonly REST_RequestService _requestService;

    public MainWindowViewModel()
    {
        var logger = new NullLogger<REST_RequestService>();
        _requestService = new REST_RequestService(logger, "communicationsettings.json");

        SearchCommand = new RelayCommand(async () => await SearchWeather());

        DayOptions = new ObservableCollection<string>
        {
            DateTime.Now.Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(1).Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(2).Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(3).Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(4).Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(5).Date.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(6).Date.ToString("yyyy-MM-dd")
        };
    }

    public string CityName
    {
        get => _cityName;
        set
        {
            _cityName = value;
            OnPropertyChanged(nameof(CityName));
        }
    }

    public string WeatherData
    {
        get => _weatherData;
        set
        {
            _weatherData = value;
            OnPropertyChanged(nameof(WeatherData));
        }
    }

    public string SelectDay
    {
        get => _selectDay;
        set
        {
            _selectDay = value;
            OnPropertyChanged(nameof(SelectDay));
        }
    }

    public ObservableCollection<string> DayOptions { get; set; }

    public ICommand SearchCommand { get; }

    #region private methods

    private async Task<ToClientWeatherDataResponse> GetWeatherResponse()
    {
        string[] args = [CityName, SelectDay];

        return await _requestService.ExecuteRequestAsync<ToClientWeatherDataResponse>(
                "GetWeatherData",
                RequestType.GET,
                null,
                args
        );
    }

    private string MapWeatherData(ToClientWeatherDataResponse response)
    {
        ToClientWeatherDataDay weatherDay = null;
        IEnumerable<ToClientWeatherDataHour> weatherHours = null;

        if (response.Days.Count > 0)
            weatherDay = response.Days.First();

        if (weatherDay.Hours.Count > 0)
            weatherHours = weatherDay.Hours;

        string weatherData = $"Address : {response.ResolvedAddress} - " +
                             $"TimeZone : {response.TimeZone} - " +
                             $"Date and Time : {weatherDay.DateTime} - " +
                             $"Precipitation Probability : {weatherDay.PrecipProb}\n\n";

        foreach (ToClientWeatherDataHour hour in weatherHours)
        {
            weatherData += $"Hour : {hour.DateTime}  -  " +
                           $"Temperature : {hour.Temp}  -  " +
                           $"Perceived Temp : {hour.FeelsLike}  -  " +
                           $"Humidity : {hour.Humidity}\n\n";
        }

        return weatherData;
    }

    private async Task SearchWeather()
    {
        try
        {
            ToClientWeatherDataResponse response = await GetWeatherResponse();

            WeatherData = MapWeatherData(response);
        }
        catch (Exception ex)
        {
            WeatherData = $"Error: {ex.Message}";
        }
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
