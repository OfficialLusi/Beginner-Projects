using WeatherWebApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration file
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
        builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //builder.Services.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration = builder.Configuration.GetSection("Redis")["ConnectionString"];
        //});
        builder.Services.AddLogging();
        builder.Services.AddControllers();

#if DEBUG
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
#endif


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}