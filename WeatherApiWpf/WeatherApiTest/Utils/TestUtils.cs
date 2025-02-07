using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;
using WeatherWebApi.Services;

namespace WeatherApiTest.Utils
{
    public class TestUtils
    {
        public const string _filePathHomePc = "C:\\Users\\aless\\Documents\\1- programmazione\\c# projects\\backend-projects\\Beginner-projects\\WeatherApiWpf\\WeatherApiTest\\MockFile\\ApiJson.json";
        public const string _filePathWorkPc = "E:\\repositories\\zPersonale\\backend projects\\backend-projects\\Beginner-projects\\WeatherApiWpf\\WeatherApiTest\\MockFile\\ApiJson.json";

        public HttpResponseMessage GetMockHttpClient()
        {
            string filePath = _filePathWorkPc;
            string jsonString = File.ReadAllText(filePath);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonString)
            };
        }

        public Mock<IConfiguration> GetMockConfiguration()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock
                .Setup(config => config["WeatherAPI:BaseUrl"])
                .Returns("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline");

            configurationMock
                .Setup(config => config["WeatherAPI:ApiKey"])
                .Returns("8XJ8EHEC4L4MXC22ZFHB3BM53");

            return configurationMock;
        }

        public NullLogger<WeatherApiClient> GetMockNullLoggerClient()
        {
            return new NullLogger<WeatherApiClient>();
        }
        public NullLogger<WeatherApiService> GetMockNullLoggerService()
        {
            return new NullLogger<WeatherApiService>();
        }

        public Mock<WeatherApiClient> GetWeatherClient(Mock<HttpClient> client, Mock<IConfiguration> config, NullLogger<WeatherApiClient> logger)
        {
            return new Mock<WeatherApiClient>(client.Object, config.Object, logger);
        }

        public WeatherApiService GetWeatherService(IWeatherApiClient client, NullLogger<WeatherApiService> logger, IMapper mapper)
        {
            return new WeatherApiService(client, logger, mapper);
        }


        public Mock<HttpClient> GetHttpClient(HttpMessageHandler handlerMock)
        {
            return new Mock<HttpClient>(handlerMock);
        }
    }

    public class HttpResponseManager : HttpMessageHandler
    {
        private readonly HttpResponseMessage _mockResponse;
        public HttpResponseManager(HttpResponseMessage mockResponse)
        {
            _mockResponse = mockResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mockResponse);
        }
    }
}
