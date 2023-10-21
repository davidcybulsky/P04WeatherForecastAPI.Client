using System;

namespace P04WeatherForecastAPI.Client.Forecast
{
    public class HourlyForecast
    {
        public DateTime DateTime { get; set; }
        public int EpochDateTime { get; set; }
        public int WeatherIcon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
        public bool IsDaylight { get; set; }
        public TempValue Temperature { get; set; }
    }
}
