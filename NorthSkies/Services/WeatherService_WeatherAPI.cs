using NorthSkies.DTOs;
using NorthSkies.Models;
using NorthSkies.Services.Mapping;
using System.Text.Json;

namespace NorthSkies.Services
{
    /* AL - fetches weather data from the API server
     * 
     * This class pulls data from the API
     * The data is stored in the DTOs whose structure matches with the API response
     * WeatherMapper then moves the data in the DTOs into our models.
     */

    public class WeatherService_WeatherAPI : IWeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly WeatherMapper_WeatherAPI _wheatherMapper;
        private readonly CityMapper_WeatherAPI _cityMapper;


        public WeatherService_WeatherAPI(string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = "https://api.weatherapi.com/v1";
            _wheatherMapper = new WeatherMapper_WeatherAPI();
            _cityMapper = new CityMapper_WeatherAPI();
        }

       
        public async Task<WeatherData> GetCurrentWeatherAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&aqi=no";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _wheatherMapper.MapCurrent(dto.current);
        }

        public async Task<List<WeatherData>> GetHourlyForecastAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&hours=24";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _wheatherMapper.MapHourly(dto.forecast);
        }

        public async Task<List<WeatherData>> GetDailyForecastAsync(City city)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={city.Name}&days=7";
            var response = await client.GetStringAsync(url);

            var dto = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return _wheatherMapper.MapDaily(dto.forecast);
        }

        public async Task<List<City>> GetCityByNameAsync(string name)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/search.json?key={_apiKey}&q={name}";
            var response = await client.GetStringAsync(url);
            var dto = JsonSerializer.Deserialize<List<CityDto>>(response);
            return _cityMapper.MapCities(dto);
        }
    }
}