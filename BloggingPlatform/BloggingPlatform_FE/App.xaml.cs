using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Services;
using BloggingPlatform_FE.ViewModels;
using BloggingPlatform_FE.Views;
using LusiUtilsLibrary.Backend.APIs_REST;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
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
            string workingDirectory = Environment.CurrentDirectory;
            string requestFileName = "\\communicationsettings.json";
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName + requestFileName;

            // logging subscription
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // services subscription
            services.AddSingleton<REST_RequestService>(provider =>
            {
                ILogger<REST_RequestService> logger = provider.GetRequiredService<ILogger<REST_RequestService>>();

                return new REST_RequestService(logger, projectDirectory);
            });

            services.AddSingleton<RequestService_FE>(provider =>
            {
                ILogger<RequestService_FE> logger = provider.GetRequiredService<ILogger<RequestService_FE>>();
                REST_RequestService requestService = provider.GetRequiredService<REST_RequestService>();
                return new RequestService_FE(logger, requestService);
            });

            services.AddTransient<INavigationService, NavigationService>(provider =>
            {
                MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                return new NavigationService(provider, mainWindow);
            });

            // ViewModels subscription
            services.AddTransient<MainViewModel>(provider =>
            {
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                return new MainViewModel(navigationService);
            });
            services.AddTransient<LoginViewModel>(provider =>
            {
                RequestService_FE requestService = provider.GetRequiredService<RequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();

                return new LoginViewModel(requestService, navigationService);
            });
            services.AddTransient<SignupViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<PersonalPostViewModel>();
            services.AddTransient<WritePostViewModel>();
            services.AddTransient<LoginSignupDialogViewModel>();

            // Views subscription
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<SignupView>();
            services.AddTransient<HomeView>();
            services.AddTransient<LoginSignupDialogView>();
        }
    }

}
