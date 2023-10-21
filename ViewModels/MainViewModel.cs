using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Interfaces;
using P04WeatherForecastAPI.Client.Models;
using System.Collections.ObjectModel;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel _selectedCity;
        private Weather _weather;
        private readonly IWeather _accuWeatherService;
        //public ICommand LoadCitiesCommand { get;  }


        public MainViewModel(IWeather accuWeatherService)
        {
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>();
        }

        [ObservableProperty]
        private WeatherViewModel weatherView;


        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }


        private async void LoadWeather()
        {
            if (SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key);
                WeatherView = new WeatherViewModel(_weather);
            }
        }

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(new CityViewModel(city));
        }
    }
}
