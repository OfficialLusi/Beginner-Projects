using BloggingPlatform_FE.ViewModels;
using System.Windows;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            // todo fondamentale, risolvendo le dipendenze con di, invece di passare qua l'oggetto nuovo,
            // qli passo un oggetto passato dal costruttore, che verrà automaticamente risolto
            // se sottoscritto in app.xaml.cs
            DataContext = viewModel;
        }
    }
}