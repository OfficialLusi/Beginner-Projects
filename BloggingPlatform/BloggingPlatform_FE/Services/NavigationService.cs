using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingPlatform_FE.Services
{
    public class NavigationService : INavigationService
    {

        private readonly IServiceProvider _serviceProvider;
        private MainWindow? _mainWindow = null;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void OnInitialize()
        {
            _mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        }

        public void NavigateTo(string viewName)
        {

            if (_mainWindow == null)
                OnInitialize();

            switch (viewName)
            {
                case "Home":
                    HomeView homeView = _serviceProvider.GetRequiredService<HomeView>();
                    _mainWindow.MainFrame.Navigate(homeView);  
                    break;
                case "Login":
                    LoginView loginView = _serviceProvider.GetRequiredService<LoginView>();
                    _mainWindow.MainFrame.Navigate(loginView);
                    break;
                case "Signup":
                    SignupView signupView = _serviceProvider.GetRequiredService<SignupView>();
                    _mainWindow.MainFrame.Navigate(signupView);
                    break;
                //case "PersonalPosts":
                //    PersonalPostView personalPostView = _serviceProvider.GetService<PersonalPostView>();
                //    _mainWindow.MainFrame.Navigate(personalPostView);
                //    break;
                //case "WritePost":
                //    WritePostView writePostView = _serviceProvider.GetService<WritePostView>();
                //    _mainWindow.MainFrame.Navigate(writePostView);
                //    break;

            }
        }
    }
}
