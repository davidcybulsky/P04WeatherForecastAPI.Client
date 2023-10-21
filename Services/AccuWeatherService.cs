﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Forecast;
using P04WeatherForecastAPI.Client.Interfaces;
using P04WeatherForecastAPI.Client.Models;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public class AccuWeatherService : IWeather
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language{2}";
        private const string one_day_of_daily_forecast = "forecasts/v1/daily/1day/{0}?apikey={1}&language{2}";
        private const string five_days_of_daily_forecast = "forecasts/v1/daily/5day/{0}?apikey={1}&language{2}";
        private const string one_hour_of_hourly_forecast = "forecasts/v1/hourly/1hour/{0}?apikey={1}&language{2}";
        private const string twelve_hours_of_hourly_forecast = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language{2}";
        private const string historical_current_conditions_twenty_four_hours = "currentconditions/v1/{0}/historical/24?apikey={1}&language{2}";
        private const string historical_current_conditions_six_hours = "currentconditions/v1/{0}/historical?apikey={1}&language{2}";

        private readonly string api_key;
        private readonly string language;

        public AccuWeatherService()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json");

            var configuration = builder.Build();
            api_key = configuration["api_key"];
            language = configuration["default_language"];
        }


        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;

            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

        public async Task<Weather[]> GetSixHourHistoricalConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_current_conditions_six_hours, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers;
            }
        }

        public async Task<Weather[]> GetTwentyFourHourHistoricalConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_current_conditions_twenty_four_hours, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers;
            }
        }

        public async Task<DailyForecast> GetOneDayDailyWeather(string cityKey)
        {
            string uri = base_url + "/" + string.Format(one_day_of_daily_forecast, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                DailyForecastResponse response1 = JsonConvert.DeserializeObject<DailyForecastResponse>(json);
                return response1.DailyForecasts.FirstOrDefault();
            }
        }

        public async Task<DailyForecast[]> GetFiveDayDailyWeather(string cityKey)
        {
            string uri = base_url + "/" + string.Format(five_days_of_daily_forecast, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                DailyForecastResponse response1 = JsonConvert.DeserializeObject<DailyForecastResponse>(json);
                return response1.DailyForecasts;
            }
        }

        public async Task<HourlyForecast> GetOneHourHourlyWeather(string cityKey)
        {
            string uri = base_url + "/" + string.Format(one_hour_of_hourly_forecast, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                HourlyForecast[] forecast = JsonConvert.DeserializeObject<HourlyForecast[]>(json);
                return forecast.FirstOrDefault();
            }
        }

        public async Task<HourlyForecast[]> GetTwelveHourHourlyWeather(string cityKey)
        {
            string uri = base_url + "/" + string.Format(twelve_hours_of_hourly_forecast, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                HourlyForecast[] forecast = JsonConvert.DeserializeObject<HourlyForecast[]>(json);
                return forecast;
            }
        }

    }
}
