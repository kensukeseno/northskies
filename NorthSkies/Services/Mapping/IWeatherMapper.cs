using NorthSkies.DTOs;
using NorthSkies.Models;

namespace NorthSkies.Services.Mapping
{
    public interface IWeatherMapper
    {
        WeatherData MapCurrent(CurrentDto current);
        List<WeatherData> MapHourly(ForecastDto forecast);
        List<WeatherData> MapDaily(ForecastDto forecast);
    }
}
