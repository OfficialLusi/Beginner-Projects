using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Views;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            #region initialize checks
            InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
            #endregion
           
            _navigationService = navigationService;

            LoginButton = new RelayCommand(async () => await ChargeLoginPage());
            SignupButton = new RelayCommand(async () => await ChargeSignupPage());
        }

        public ICommand LoginButton { get; }
        public ICommand SignupButton { get; }


        public async Task ChargeLoginPage()
        {
            _navigationService.NavigateTo("Login");
        }

        public async Task ChargeSignupPage()
        {
            _navigationService.NavigateTo("Login");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
