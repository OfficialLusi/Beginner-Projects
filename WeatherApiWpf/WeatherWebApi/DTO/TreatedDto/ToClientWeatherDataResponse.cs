namespace WeatherWebApi.DTO.TreatedDto
{
    public class ToClientWeatherDataResponse
    {
        public string ResolvedAddress { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ToClientWeatherDataDay> Days { get; set; }
    }
}
