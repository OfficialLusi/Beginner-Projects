using System.ComponentModel;
using System.Windows.Input;
using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Services;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.AspNetCore.Http;

namespace BloggingPlatform_FE.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email;
    private string _password;
    private RequestService_FE _requestService;
    private INavigationService _navigationService;


    public LoginViewModel(RequestService_FE requestService, INavigationService navigationService)
    {
        #region initialize checks
        InitializeChecks.InitialCheck(requestService, "Request service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation service cannot be null");
        #endregion

        _requestService = requestService;
        _navigationService = navigationService;

        LoginCommand = new RelayCommand(async () => await UserLogin());
        NavigateToSignupCommand = new RelayCommand(async () => await UserSignup());
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

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand NavigateToSignupCommand { get; }

    #region private methods

    public async Task UserLogin()
    {
        UserDto user = new UserDto
        {
            UserEmail = _email,
            UserPassword = _password
        };
        if(_requestService.AuthenticateUser(user) == StatusCodes.Status200OK);


        _navigationService.NavigateTo("Home");
    }


    public async Task UserSignup()
    {

    }

    #endregion


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
