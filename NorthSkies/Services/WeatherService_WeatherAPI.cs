using System.Text.Json;
using NorthSkies.Models;
using NorthSkies.Models.Enums;
using System;

namespace NorthSkies.Services
{

    public class WeatherService_WeatherAPI : IWeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public WeatherService_WeatherAPI(string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = "https://api.weatherapi.com/v1";
        }
        
        public async Task<WeatherData> GetCurrentWeatherAsync(City city)
        {
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&aqi=no";
                var response = await client.GetStringAsync(url);
                // Parse response and return WeatherData
                return ParseCurrentWeather(response);
            }
        }

        
        public async Task<List<WeatherData>> GetHourlyForecastAsync(City city)
        {
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city}&hours=24";
                var response = await client.GetStringAsync(url);
                // Parse response and return list of WeatherData
                return ParseHourlyForecast(response);
            }
        }

        public async Task<List<WeatherData>> GetDailyForecastAsync(City city)
        {
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city}&days=7";
                var response = await client.GetStringAsync(url);
                // Parse response and return list of WeatherData
                return ParseDailyForecast(response);
            }
        }

        private WeatherData ParseCurrentWeather(string json)
        {
            //JSON Deserializer helps with parsing JSON responses
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var current = root.GetProperty("current");


            var timestamp = DateTime.Parse(current.GetProperty("last_updated").GetString());
            var tempF = current.GetProperty("temp_f").GetDouble();
            var tempC = current.GetProperty("temp_c").GetDouble();
            var feelsLikeF = current.GetProperty("feelslike_f").GetDouble();
            var feelsLikeC = current.GetProperty("feelslike_c").GetDouble();
            var humidity = current.GetProperty("humidity").GetInt32();
            var windMPH = current.GetProperty("wind_mph").GetDouble();
            var windKPH = current.GetProperty("wind_kph").GetDouble();
            var conditionText = current.GetProperty("condition").GetProperty("text").GetString();
            var precipitationChance = current.TryGetProperty("precip_mm", out var precip) ? precip.GetDouble() : (double?)null;

            return new WeatherData(
                timestamp,
                tempF,
                tempC,
                feelsLikeF,
                feelsLikeC,
                humidity,
                windMPH,
                windKPH,
                MapCondition(conditionText),
                precipitationChance
            );
        }


        private List<WeatherData> ParseHourlyForecast(string json)
        {
            // Implement JSON parsing logic to convert to list of WeatherData
            return new List<WeatherData>();
        }

        private List<WeatherData> ParseDailyForecast(string json)
        {
            // Implement JSON parsing logic to convert to list of WeatherData
            return new List<WeatherData>();
        }



        private WeatherCondition MapCondition(string text)
        {
            return text.ToLower().Trim() switch
            {
                "sunny" => WeatherCondition.Sunny,
                "cloudy" => WeatherCondition.Cloudy,
                "overcast" => WeatherCondition.Overcast,
                "rain" => WeatherCondition.Rain,
                "snow" => WeatherCondition.Snow,
                _ => WeatherCondition.Cloudy
            };
        }

    }

}