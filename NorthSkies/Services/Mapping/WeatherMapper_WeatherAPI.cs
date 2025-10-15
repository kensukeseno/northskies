using System;
using System.Collections.Generic;
using NorthSkies.DTOs;
using NorthSkies.Models;
using NorthSkies.Models.Enums;

namespace NorthSkies.Services.Mapping
{
    public class WeatherMapper_WeatherAPI : IWeatherMapper
    {
        

        public WeatherData MapCurrent(CurrentDto current)
        {
            if (current == null) return null;

            // Look up the condition object from the WeatherAPI code
            var condition = WeatherCondition.FromCode(current.condition.code);

            // Decide which text/icon to use based on is_day
            bool isDay = current.is_day == 1;

            return new WeatherData(
                timestamp: ParseDate(current.last_updated),
                tempF: current.temp_f,
                tempC: current.temp_c,
                feelsLikeF: current.feelslike_f,
                feelsLikeC: current.feelslike_c,
                humidity: current.humidity,
                windSpeedMPH: current.wind_mph,
                windSpeedKPH: current.wind_kph,
                condition: condition,
                precipitationChance: null
            );
        }

        public List<WeatherData> MapHourly(ForecastDto forecast)
        {
            var result = new List<WeatherData>();
            if (forecast?.forecastday == null) return result;

            foreach (var day in forecast.forecastday)
            {
                if (day.hour == null) continue;

                foreach (var h in day.hour)
                {
                    var condition = WeatherCondition.FromCode(h.condition.code);
                    //bool isDay = h.is_day == 1;
                    bool isDay = true;

                    result.Add(new WeatherData(
                        timestamp: ParseDate(h.time),
                        tempF: h.temp_f,
                        tempC: h.temp_c,
                        feelsLikeF: h.feelslike_f,
                        feelsLikeC: h.feelslike_c,
                        humidity: h.humidity,
                        windSpeedMPH: h.wind_mph,
                        windSpeedKPH: h.wind_kph,
                        condition: condition,
                        precipitationChance: null
                    ));

                }
            }

            return result;
        }


        public List<WeatherData> MapDaily(ForecastDto forecast)
        {
            var result = new List<WeatherData>();
            if (forecast?.forecastday == null) return result;

            foreach (var day in forecast.forecastday)
            {
                var d = day.day;
                if (d == null) continue;

                // Look up the condition object from the WeatherAPI code
                var condition = WeatherCondition.FromCode(d.condition.code);

                // For daily summaries, WeatherAPI doesn’t give is_day,
                // so we’ll default to the "day" variant for text/icon.
                bool isDay = true;

                // Use the date at noon as the representative timestamp
                var timestamp = ParseDate($"{day.date} 12:00");

                result.Add(new WeatherData(
                    timestamp: timestamp,
                    tempF: d.avgtemp_f,
                    tempC: d.avgtemp_c,
                    feelsLikeF: d.avgtemp_f,
                    feelsLikeC: d.avgtemp_c,
                    humidity: 0, // daily summary doesn’t include humidity
                    windSpeedMPH: 0,  // daily summary doesn’t include wind
                    windSpeedKPH: 0,
                    condition: condition,              // full WeatherCondition object
                    precipitationChance: null          // could be extended if you want to use chance_of_rain/snow
                ));

                // UI layer can later do:
                // string label = isDay ? condition.DayText : condition.NightText;
                // string iconUrl = condition.GetIconUrl(isDay);
            }

            return result;
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.Parse(value);
        }

        

    }
}