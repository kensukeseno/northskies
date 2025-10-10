using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;

namespace NorthSkies.Models
{
    internal class WeatherData
    {
        public DateTime TimeStamp { get; }
        public double Temperature { get; }
        public double FeelsLike { get; }
        public double Humidity { get; }
        public double WindSpeed { get; }
        public WeatherCondition Condition { get; }
        public double? PrecipitationChance { get; }
        public UnitSystem UnitSystem { get; }

        public WeatherData(DateTime timestamp, double temperature, double feelsLike, double humidity, double windSpeed, WeatherCondition condition, double precipitationChance)
        {
            TimeStamp = timestamp;
            Temperature = temperature;
            FeelsLike = feelsLike;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Condition = condition;
            PrecipitationChance = precipitationChance;
        }

    }
}
