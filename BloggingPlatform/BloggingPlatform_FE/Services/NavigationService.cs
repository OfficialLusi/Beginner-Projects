using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingPlatform_FE.Services
{
    public class NavigationService : INavigationService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly MainWindow _mainWindow;

        public NavigationService(IServiceProvider serviceProvider, MainWindow mainWindow)
        {
            _serviceProvider = serviceProvider;
            _mainWindow = mainWindow;
        }

        public void NavigateTo(string viewName)
        {
            switch (viewName)
            {
                case "Home":
                    HomeView homeView = _serviceProvider.GetService<HomeView>();
                    _mainWindow.MainFrame.Navigate(homeView);  
                    break;
                case "Login":
                    LoginView loginView = _serviceProvider.GetService<LoginView>();
                    _mainWindow.MainFrame.Navigate(loginView);
                    break;
                case "Signup":
                    SignupView signupView = _serviceProvider.GetService<SignupView>();
                    _mainWindow.MainFrame.Navigate(signupView);
                    break;
            }
        }
    }
}
