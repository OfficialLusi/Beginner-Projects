
namespace WeatherWebApi.DTO.TreatedDto
{
    public class SharedDataDay
    {
        public string DateTime { get; set; } = string.Empty;
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public double Temp { get; set; }
        public double FeelsLikeMax { get; set; }
        public double FeelsLikeMin { get; set; }
        public double FeelsLike { get; set; }
        public double Humidity { get; set; }
        public double Precip { get; set; }
        public double PrecipProb { get; set; }
        public double PrecipCover { get; set; }
        public List<string>? PrecipType { get; set; }
        public double Snow { get; set; }
        public string Sunrise { get; set; } = string.Empty;
        public string Sunset { get; set; } = string.Empty;
        public string DescriptionDay { get; set; } = string.Empty;
        public required List<SharedDataHour> Hours { get; set; }
    }
}
