using P04WeatherForecastAPI.Client.ViewModels;
using System.Windows;

namespace P04WeatherForecastAPI.Client
{

    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}