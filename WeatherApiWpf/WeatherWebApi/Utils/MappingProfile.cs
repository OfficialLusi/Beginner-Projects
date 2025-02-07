//using AutoMapper;
//using WeatherWebApi.DTO.ApiDto;
//using WeatherWebApi.DTO.TreatedDto;

//namespace WeatherWebApi.Utils;

//public class MappingProfile : Profile
//{
//    // crating the mapping profile to correclty map the classes
//    public MappingProfile()
//    {
//        CreateMap<WeatherResponse, ToClientWeatherDataResponse>()
//            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionDay));

//        CreateMap<WeatherDays, ToClientWeatherDataDay>();
//        CreateMap<WeatherHours, ToClientWeatherDataHour>();
//    }
//}
