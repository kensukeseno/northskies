using System;
using System.Collections.Generic;
using NorthSkies.DTOs;
using NorthSkies.Models;
using NorthSkies.Models.Enums;

namespace NorthSkies.Services.Mapping
{
    public class WeatherMapper_WeatherAPI : IWeatherMapper
    {
        /* AL - parses the three main structure in the API response - current weather, daily forecast, and hourly forecast
         * 
         * The WeatherService pulls data from the API
         * The data is stored in the DTOs whose structure matches with the API response
         * This class then moves the data in the DTOs into our models.
         */

        public WeatherData MapCurrent(CurrentDto current)
        {
            if (current == null) return null;

            // Look up the condition object from the WeatherAPI code
            var condition = WeatherCondition.FromCode(current.condition.code);

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

            foreach (var day in forecast.forecastday) //loop through the days
            {
                if (day.hour == null) continue;

                foreach (var h in day.hour) //loop through the hours per day
                {
                    var condition = WeatherCondition.FromCode(h.condition.code);

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

                // Use the date at noon as the representative timestamp
                var timestamp = ParseDate($"{day.date} 12:00");

                result.Add(new WeatherData(
                    timestamp: timestamp,
                    tempF: d.avgtemp_f,
                    tempC: d.avgtemp_c,
                    feelsLikeF: d.avgtemp_f,
                    feelsLikeC: d.avgtemp_c,
                    humidity: 0, // getting an error with this, and can't compile so i set it to 0
                    windSpeedMPH: 0, // daily summary doesn’t include wind. boooo
                    windSpeedKPH: 0,
                    condition: condition,
                    precipitationChance: null
                ));

            }

            return result;
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.Parse(value);
        }

        

    }
}