using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;

namespace NorthSkies.Models
{
    /* AL extends current data with forecasted information:
     * Minimum Temp, Maximum Temp, Average Temp - in C and F
    */
    internal class ForecastData : WeatherData
    {
        public double MinTempF { get; }
        public double MaxTempF { get; }
        public double AveTempF { get; }
        public double MinTempC { get; }
        public double MaxTempC { get; }
        public double AveTempC { get; }

        public ForecastData(
            DateTime timestamp,
            double tempF,
            double tempC,
            double feelsLikeF,
            double feelsLikeC,
            double humidity,
            double windSpeedMPH,
            double windSpeedKPH,
            WeatherCondition condition,
            double? precipitationChance,
            double minTempF,
            double maxTempF,
            double aveTempF,
            double minTempC,
            double maxTempC,
            double aveTempC
        ) : base(timestamp, tempF, tempC, feelsLikeF, feelsLikeC, humidity, windSpeedMPH, windSpeedKPH, condition, precipitationChance)
        {
            MinTempF = minTempF;
            MaxTempF = maxTempF;
            AveTempF = aveTempF;
            MinTempC = minTempC;
            MaxTempC = maxTempC;
            AveTempC = aveTempC;
        }


    }
}
