using System.ComponentModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Input;
using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;

namespace BloggingPlatform_FE.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _userInfo;
    private IRequestService_FE _requestService;
    private INavigationService _navigationService;


    public LoginViewModel(IRequestService_FE requestService, INavigationService navigationService)
    {
        #region initialize checks
        InitializeChecks.InitialCheck(requestService, "Request service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation service cannot be null");
        #endregion

        _requestService = requestService;
        _navigationService = navigationService;

        LoginCommand = new RelayCommand<object>(async (param) => await UserLogin(param), (param) => true);
        NavigateToSignupCommand = new RelayCommand(() => UserSignup());
    }

    public string UserInfo
    {
        get => _userInfo;
        set
        {
            _userInfo = value;
            OnPropertyChanged(nameof(UserInfo));
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand NavigateToSignupCommand { get; }

    #region private methods

    public async Task UserLogin(object parameter)
    {
        UserDto user = new UserDto();

        if (parameter is PasswordBox pb)
            user.UserPassword = pb.Password;

        if (_userInfo.Contains('@'))
            user.UserEmail = _userInfo;
        else
            user.UserName = _userInfo;

        ApiResponse<UserDto> data = await _requestService.AuthenticateUser(user);

        if (data.StatusCode == HttpStatusCode.OK)
        {
            _navigationService.NavigateTo("Home");
            // eventually take the user here
            return;
        }

        // todo: call here the login error pupup.

    }


    public async Task UserSignup()
    {
        _navigationService.NavigateTo("Signup");
    }

    #endregion


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
