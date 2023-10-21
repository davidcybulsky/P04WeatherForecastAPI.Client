using P04WeatherForecastAPI.Client.Forecast;
using P04WeatherForecastAPI.Client.Models;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Interfaces
{
    public interface IWeather
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<Weather[]> GetSixHourHistoricalConditions(string cityKey);
        Task<Weather[]> GetTwentyFourHourHistoricalConditions(string cityKey);
        Task<DailyForecast> GetOneDayDailyWeather(string cityKey);
        Task<DailyForecast[]> GetFiveDayDailyWeather(string cityKey);
        Task<HourlyForecast> GetOneHourHourlyWeather(string cityKey);
        Task<HourlyForecast[]> GetTwelveHourHourlyWeather(string cityKey);
    }
}
