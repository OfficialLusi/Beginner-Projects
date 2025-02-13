using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Services;
using BloggingPlatform_FE.ViewModels;
using BloggingPlatform_FE.Views;
using LusiUtilsLibrary.Backend.APIs_REST;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace BloggingPlatform_FE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            services.AddSingleton<REST_RequestService>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<REST_RequestService>>();
                return new REST_RequestService(logger, "communicationsettings.json");
            });

            // ViewModels subscription
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SignupViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<PersonalPostViewModel>();
            services.AddTransient<WritePostViewModel>();

            // Registra le finestre, se vuoi risolverle tramite DI
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<SignupViewModel>();

            // Configura anche il logging
            services.AddLogging(configure => configure.AddConsole());
        }

}
