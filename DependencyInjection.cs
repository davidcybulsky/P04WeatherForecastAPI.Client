using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Interfaces;
using P04WeatherForecastAPI.Client.Services;

namespace P04WeatherForecastAPI.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IWeather, AccuWeatherService>();
            services.AddTransient<MainWindow>();
            return services;
        }
    }
}
