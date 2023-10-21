using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Interfaces;
using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client.ViewModels;

namespace P04WeatherForecastAPI.Client
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWeather, AccuWeatherService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}
