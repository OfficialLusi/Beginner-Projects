using BloggingPlatform_BE.Application.Services;
using BloggingPlatform_BE.Domain.Interfaces;
using BloggingPlatform_BE.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

internal class Program
{
    private static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;

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
            ILogger<IRepositoryService> repoLogger = provider.GetRequiredService<ILogger<IRepositoryService>>();

            return new RepositoryService(connectionString, dbName, repoLogger);
        });

        builder.Services.AddTransient<AuthenticationService>(provider =>
        {
            // Getting repository service from container
            IRepositoryService repositoryService = provider.GetRequiredService<IRepositoryService>();

            return new AuthenticationService(repositoryService);
        });

        builder.Services.AddSingleton<IApplicationService>(provider =>
        {
            // Getting dependencies from di container
            IRepositoryService repositoryService = provider.GetRequiredService<IRepositoryService>();
            AuthenticationService authService = provider.GetRequiredService<AuthenticationService>();
            ILogger<ApplicationService> appLogger = provider.GetRequiredService<ILogger<ApplicationService>>();

            return new ApplicationService(dbPath, repositoryService, baseDir, appLogger, authService);
        });



        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        using (IServiceScope scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            IApplicationService applicationService = services.GetRequiredService<IApplicationService>();
            CreateDatabase(applicationService);
        }

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

    private static void CreateDatabase(IApplicationService service)
    {
        service.CreateDataBase();
    }
}