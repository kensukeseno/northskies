using System;
using System.Text.Json;
using NorthSkies.Models;
using NorthSkies.Services.Mapping;
using NorthSkies.DTOs;


namespace NorthSkies.Services
{

    public class WeatherService_WeatherAPI : IWeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly WeatherMapper_WeatherAPI _mapper;


        public WeatherService_WeatherAPI(string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = "https://api.weatherapi.com/v1";
            _mapper = new WeatherMapper_WeatherAPI();
        }

       
        public async Task<WeatherData> GetCurrentWeatherAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&aqi=no";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _mapper.MapCurrent(dto.current);
        }

        public async Task<List<WeatherData>> GetHourlyForecastAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&hours=24";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _mapper.MapHourly(dto.forecast);
        }

        public async Task<List<WeatherData>> GetDailyForecastAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&days=7";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _mapper.MapDaily(dto.forecast);
        }


    }

}