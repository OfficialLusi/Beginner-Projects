using System.ComponentModel;
using System.Windows.Input;
using LusiUtilsLibrary.Frontend.MVVMHelpers;

namespace BloggingPlatform_FE.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email;
    private string _password;

    public LoginViewModel()
    {
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
