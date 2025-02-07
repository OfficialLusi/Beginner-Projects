namespace WeatherWebApi.DTO.TreatedDto
{
    public class SharedData
    {
        public string ResolvedAddress { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SharedDataDay> Days { get; set; }
    }
}
