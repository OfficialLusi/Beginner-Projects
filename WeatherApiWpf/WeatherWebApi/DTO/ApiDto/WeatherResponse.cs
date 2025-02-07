namespace WeatherWebApi.DTO.ApiDto
{
    public class WeatherResponse
    {
        #region public properties

        public int QueryCost { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ResolvedAddress { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public double TzOffset { get; set; }
        public string DescriptionDay { get; set; } = string.Empty;
        public required IEnumerable<WeatherDays> Days { get; set; }

        #endregion
    }
}
