using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_FE.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using System.Reflection.Metadata;
using System.Windows.Controls;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels
{
    public class SignupViewModel
    {
        private IRequestService_FE _requestService;
        private INavigationService _navigationService;


        public SignupViewModel(IRequestService_FE requestService, INavigationService navigationService)
        {
            #region initialize checks
            InitializeChecks.InitialCheck(requestService, "Request service cannot be null");
            InitializeChecks.InitialCheck(navigationService, "Navigation service cannot be null");
            #endregion

            _requestService = requestService;
            _navigationService = navigationService;

            SignupCommand = new RelayCommand<object>(async (param) => await UserSignup(param), (param) => true);
            //NavigateToSignupCommand = new RelayCommand(async () => await UserSignup());
        }



        public ICommand SignupCommand { get; }

        public async Task UserSignup(object parameter)
        {
            string password = string.Empty;
            if (parameter is PasswordBox pb)
            {
                password = pb.Password;
            }

            UserDto user = new UserDto()
            {
                UserName = 
            };
        }

    }
}
