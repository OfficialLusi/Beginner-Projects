using System.Reflection.Metadata.Ecma335;

namespace WeatherWebApi.DTO.ApiDto
{
    public class WeatherHours
    {
        #region private fields

        private double _temp;
        private double _feelsLike;

        #endregion

        #region public properties

        public string DateTime { get; set; } = string.Empty;
        public long DateTimeEpoch { get; set; }
        public double Temp { get => _temp; set => _temp = ConvertFahrenheitToCelsius(value); }
        public double FeelsLike { get => _feelsLike; set => _feelsLike = ConvertFahrenheitToCelsius(value); }
        public double Humidity { get; set; }
        public double Dew { get; set; }
        public double Precip { get; set; }
        public double PrecipProb { get; set; }
        public double Snow { get; set; }
        public double SnowDepth { get; set; }
        public List<string>? PrecipType { get; set; }
        public double WindGust { get; set; }
        public double WindSpeed { get; set; }
        public double WindDir { get; set; }
        public double Pressure { get; set; }
        public double Visibility { get; set; }
        public double CloudCover { get; set; }
        public double SolarRadiation { get; set; }
        public double SolarEnergy { get; set; }
        public double UvIndex { get; set; }
        public double SevereRisk { get; set; }
        public string ConditionsHour { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public List<string>? Stations { get; set; }
        public string Source { get; set; } = string.Empty;

        #endregion

        #region Converting Method
        private double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            return Math.Round((fahrenheit - 32) * 5 / 9, 2);
        }
        #endregion
    }
}
