using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;

namespace NorthSkies.Models
{
    /* AL - main model for weather data : current
     */
    public class WeatherData
    {
        public DateTime TimeStamp { get; }
        public double TempF { get; }
        public double TempC { get; }
        public double FeelsLikeF { get; }
        public double FeelsLikeC { get; }
        public double Humidity { get; }
        public double WindSpeedMPH { get; }
        public double WindSpeedKPH { get; }
        public WeatherCondition Condition { get; }
        public double? PrecipitationChance { get; }

        public WeatherData(DateTime timestamp, double tempF, double tempC, double feelsLikeF, double feelsLikeC, double humidity, double windSpeedMPH, double windSpeedKPH, WeatherCondition condition, double? precipitationChance)
        {
            TimeStamp = timestamp;
            TempF = tempF;
            TempC = tempC;
            FeelsLikeF = feelsLikeF;
            FeelsLikeC = feelsLikeC;
            Humidity = humidity;
            WindSpeedMPH = windSpeedMPH;
            WindSpeedKPH = windSpeedKPH;
            Condition = condition;
            PrecipitationChance = precipitationChance;
        }

    }
}
