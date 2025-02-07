using AutoMapper;
using WeatherWebApi.DTO.ApiDto;
using WeatherWebApi.DTO.TreatedDto;

namespace WeatherWebApi.Utils;

public class WeatherServicesMapper(IMapper mapper)
{
    //private readonly IMapper _mapper = mapper;

    public ToClientWeatherDataResponse ConvertFromApiDtoToTreatedDto(WeatherResponse apiResponse, string date)
    {
        ToClientWeatherDataResponse treatedResponse = new ToClientWeatherDataResponse();

        treatedResponse.ResolvedAddress = apiResponse.ResolvedAddress;
        treatedResponse.Address = apiResponse.Address;
        treatedResponse.TimeZone = apiResponse.TimeZone;
        treatedResponse.Description = apiResponse.DescriptionDay;

        treatedResponse.Days = apiResponse.Days
            .Where(x => Convert.ToDateTime(x.DateTime).ToString("yyyy-MM-dd") == date)
            .Select(day => new ToClientWeatherDataDay
            {
                DateTime = day.DateTime,
                TempMax = day.TempMax,
                TempMin = day.TempMin,
                Temp = day.Temp,
                FeelsLikeMax = day.FeelsLikeMax,
                FeelsLikeMin = day.FeelsLikeMin,
                FeelsLike = day.FeelsLike,
                Humidity = day.Humidity,
                Precip = day.Precip,
                PrecipProb = day.PrecipProb,
                PrecipCover = day.PrecipCover,
                PrecipType = day.PrecipType,
                Snow = day.Snow,
                Sunrise = day.Sunrise,
                Sunset = day.Sunset,
                DescriptionDay = day.DescriptionDay,
                Hours = day.Hours
                    .Select(hour => new ToClientWeatherDataHour
                    {
                        DateTime = hour.DateTime,
                        Temp = hour.Temp,
                        FeelsLike = hour.FeelsLike,
                        Humidity = hour.Humidity,
                        Precip = hour.Precip,
                        PrecipProb = hour.PrecipProb,
                        Snow = hour.Snow,
                        SnowDepth = hour.SnowDepth,
                        PrecipType = hour.PrecipType,
                        Pressure = hour.Pressure,
                        Visibility = hour.Visibility,
                        ConditionsHour = hour.ConditionsHour
                    }).ToList()
            }).ToList();

        return treatedResponse;
    }

    //public ToClientWeatherDataResponse ConvertFromApiDtoToTreatedDto(WeatherResponse apiResponse) => _mapper.Map<ToClientWeatherDataResponse>(apiResponse);
}
