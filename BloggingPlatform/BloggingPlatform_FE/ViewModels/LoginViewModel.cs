using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows.Controls;
using System.Windows.Input;
using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Services;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.AspNetCore.Http;

namespace BloggingPlatform_FE.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email;
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
        //NavigateToSignupCommand = new RelayCommand(async () => await UserSignup());
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public ICommand LoginCommand { get; }
    //public ICommand NavigateToSignupCommand { get; }

    #region private methods

    public async Task UserLogin(object parameter)
    {
        string password = string.Empty;
        if (parameter is PasswordBox pb)
        {
            password = pb.Password;
        }

        UserDto user = new UserDto
        {
            UserEmail = _email,
            UserPassword = password
        };

        ApiResponse<UserDto> data = await _requestService.AuthenticateUser(user);

        if (Convert.ToInt32(data.StatusCode) == StatusCodes.Status200OK)
        {
            _navigationService.NavigateTo("Home");
            // eventually take the user here
            return;
        }

        // todo: call here the login error pupup.

    }


    //public async Task UserSignup()
    //{
    //    _navigationService.NavigateTo("Signup");
    //}

    #endregion


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
