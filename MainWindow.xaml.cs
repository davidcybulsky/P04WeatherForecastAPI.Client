using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccuWeatherService accuWeatherService;
        public MainWindow()
        {
            InitializeComponent();
            accuWeatherService = new AccuWeatherService();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            City[] cities = await accuWeatherService.GetLocations(txtCity.Text);

            // standardowy sposób dodawania elementów
            //lbData.Items.Clear();
            //foreach (var c in cities)
            //    lbData.Items.Add(c.LocalizedName);

            // teraz musimy skorzystac z bindowania danych bo chcemy w naszej kontrolce przechowywac takze id miasta 
            lbData.ItemsSource = cities;
        }

        private async void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                //var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                lblCityName.Content = selectedCity.LocalizedName;
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValue.Content = Convert.ToString(tempValue);
                var dailyForecast = await accuWeatherService.GetOneDayDailyWeather(selectedCity.Key);
                lblDailyWeather.Content = dailyForecast.Temperature.Minimum.Value + " " + dailyForecast.Temperature.Minimum.Unit + " - " + dailyForecast.Temperature.Maximum.Value + " " + dailyForecast.Temperature.Maximum.Unit;
                var fiveDays = await accuWeatherService.GetFiveDayDailyWeather(selectedCity.Key);
                string content = "";
                foreach (var daily in fiveDays)
                {
                    content += daily.Temperature.Minimum.Value + " " + daily.Temperature.Minimum.Unit + " - " + daily.Temperature.Maximum.Value + " " + daily.Temperature.Maximum.Unit + "\n";
                }
                lblFiveDayWeather.Content = content;
                var dayhourly = await accuWeatherService.GetTwelveHourHourlyWeather(selectedCity.Key);
                content = "";
                foreach (var hourly in dayhourly)
                {
                    content += hourly.Temperature.Value + " " + hourly.Temperature.Unit + "\n";
                }
                lblHourlyWeather.Content = content;
            }
        }
    }
}
