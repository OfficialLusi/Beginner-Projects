namespace WeatherWebApi.DTO.TreatedDto
{
    public class SharedDataHour
    {
        public string DateTime { get; set; } = string.Empty;
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double Humidity { get; set; }
        public double Precip { get; set; }
        public double PrecipProb { get; set; }
        public double Snow { get; set; }
        public double SnowDepth { get; set; }
        public List<string>? PrecipType { get; set; }
        public double Pressure { get; set; }
        public double Visibility { get; set; }
        public string ConditionsHour { get; set; } = string.Empty;

    }
}
