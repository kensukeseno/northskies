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
        /* AL - Interface in case we want to use another source like a different API server
         */
        public Task<WeatherData> GetCurrentWeatherAsync(City city);
        public Task<List<WeatherData>> GetHourlyForecastAsync(City city);
        public Task<List<WeatherData>> GetDailyForecastAsync(City city);
    }

}
