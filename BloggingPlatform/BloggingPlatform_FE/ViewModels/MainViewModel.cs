using BloggingPlatform_FE.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using System.ComponentModel;

namespace BloggingPlatform_FE.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;


        public MainViewModel(INavigationService navigationService)
        {
            #region initialize checks
            InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
            #endregion
           
            _navigationService = navigationService;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
