using BloggingPlatform_FE.ViewModels;
using System.Windows;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for LoginSignupDialog.xaml
    /// </summary>
    public partial class LoginSignupDialogView : Window
    {
        public LoginSignupDialogView(LoginSignupDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

