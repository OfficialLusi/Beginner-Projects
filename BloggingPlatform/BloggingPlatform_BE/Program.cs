using BloggingPlatform_BE.Application.Services;
using BloggingPlatform_BE.Domain.Interfaces;
using BloggingPlatform_BE.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        IConfiguration configuration = builder.Configuration;

        #region sqlite db handling
        string baseDir = Directory.GetCurrentDirectory();
        string dbName = configuration["DatabaseName"];
        string dbPath = Path.Combine(baseDir, dbName);
        string connectionString = configuration.GetConnectionString("DBConnection");
        #endregion

        builder.Services.AddLogging(configure =>
        {
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Information);
        });

        builder.Services.AddSingleton<IRepositoryService>(provider =>
        {
            // Getting logger from di container
            var repoLogger = provider.GetRequiredService<ILogger<IRepositoryService>>();

            return new RepositoryService(connectionString, dbName, repoLogger);
        });

        builder.Services.AddSingleton<IApplicationService>(provider =>
        {
            // Getting dependencies from di container
            var repositoryService = provider.GetRequiredService<IRepositoryService>();
            var appLogger = provider.GetRequiredService<ILogger<ApplicationService>>();

            return new ApplicationService(dbPath, connectionString, repositoryService, baseDir, appLogger);
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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