using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models;

namespace NorthSkies.Services
{
    interface IWeatherService
    {
        public Task<WeatherData> GetCurrentWeatherAsync(City city);
        public Task<List<WeatherData>> GetHourlyForecastAsync(City city);
        public Task<List<WeatherData>> GetDailyForecastAsync(City city);
    }
    //internal class WeatherService: IWeatherService
    //{
    //    public WeatherData GetCurrentWeather(City city) { return null;  }
    //    public List<WeatherData> GetHourlyForecast(City city) { return null; }
    //    public List<WeatherData> GetDailyForecast(City city) {  return null; }
    //}
}
