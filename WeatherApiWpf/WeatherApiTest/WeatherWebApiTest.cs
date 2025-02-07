//using AutoMapper;
//using FluentAssertions;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging.Abstractions;
//using Moq;
//using Moq.Protected;
//using WeatherApiTest.Utils;
//using WeatherWebApi.DTO.ApiDto;
//using WeatherWebApi.DTO.TreatedDto;
//using WeatherWebApi.Services;

//namespace WeatherApiTest;

//public class WeatherWebApiTest
//{
//    [Fact]
//    public async Task ShouldReceiveDataFromApiAsync()
//    {
//        // Arrange
//        TestUtils utils = new TestUtils();

//        HttpResponseMessage httpResponseMessage = utils.GetMockHttpClient();
//        Mock<IConfiguration> mockConfig = utils.GetMockConfiguration();
//        NullLogger<WeatherApiClient> loggerClient = utils.GetMockNullLoggerClient();
//        NullLogger<WeatherApiService> loggerService = utils.GetMockNullLoggerService();
//        Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>();
//        Mock<IMapper> mapperMock = new Mock<IMapper>();
//        string citySetup = "Udine";

//        handlerMock
//            .Protected()
//            .Setup<Task<HttpResponseMessage>>(
//            "SendAsync",
//            ItExpr.IsAny<HttpRequestMessage>(),
//            ItExpr.IsAny<CancellationToken>()
//            )
//            .ReturnsAsync(httpResponseMessage);

//        mapperMock
//            .Setup(m => m.Map<ToClientWeatherDataResponse>(It.IsAny<WeatherResponse>()))
//            .Returns((WeatherResponse source) => new ToClientWeatherDataResponse
//            {
//                ResolvedAddress = source.ResolvedAddress,
//                TimeZone = source.TimeZone,
//            });

//        Mock<HttpClient> httpClientMock = utils.GetHttpClient(handlerMock.Object);
//        Mock<WeatherApiClient> client = utils.GetWeatherClient(httpClientMock, mockConfig, loggerClient);
//        WeatherApiService service = utils.GetWeatherService(client.Object, loggerService, mapperMock.Object);

//        // Act
//        ToClientWeatherDataResponse response = await service.GetData(citySetup);

//        // Assert
//        response.Should().NotBeNull(); 
//        response.ResolvedAddress.Should().Be("Udine, Friuli-Venezia Giulia, Italia");
//        response.TimeZone.Should().Be("Europe/Rome");
//    }
//}