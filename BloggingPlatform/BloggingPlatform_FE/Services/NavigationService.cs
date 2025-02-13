using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Views;

namespace BloggingPlatform_FE.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(string viewName)
        {
            switch (viewName)
            {
                case "Home":
                    new HomeView().Show();
                    break;
                case "Login":
                    new LoginView().Show();
                    break;
                case "Signup":
                    new SignupView().Show();
                    break;
            }
        }
    }
}
