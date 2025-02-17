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

            // rest service from library
            services.AddSingleton<IREST_RequestService, REST_RequestService>(provider =>
            {
                ILogger<REST_RequestService> logger = provider.GetRequiredService<ILogger<REST_RequestService>>();

                return new REST_RequestService(logger, projectDirectory);
            });

            // rest service for frontend
            services.AddSingleton<IRequestService_FE, RequestService_FE>(provider =>
            {
                ILogger<IRequestService_FE> logger = provider.GetRequiredService<ILogger<IRequestService_FE>>();
                IREST_RequestService requestService = provider.GetRequiredService<IREST_RequestService>();
                return new RequestService_FE(logger, requestService);
            });

            // page navigation service
            services.AddTransient<INavigationService, NavigationService>(provider =>
            {
                return new NavigationService(provider);
            });
           
            // ViewModels subscription
            services.AddTransient(provider =>
            {
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                return new MainViewModel(navigationService);
            });
            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
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
