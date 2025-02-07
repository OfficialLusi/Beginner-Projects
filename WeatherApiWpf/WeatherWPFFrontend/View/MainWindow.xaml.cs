using System.Windows;
using WeatherWPFFrontend.ViewModel;

namespace WeatherWPFFrontend;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}