using NorthSkies.DTOs;
using NorthSkies.Models;

namespace NorthSkies.Services.Mapping
{
    public class CityMapper_WeatherAPI : ICityMapper
    {
        public List<City> MapCities(List<CityDto> cities)
        {
            var result = new List<City>();
            if (cities == null) return result;

            foreach (var city in cities)
            {
                    result.Add(new City(
                        name: city.name,
                        country: city.country
                    ));
            }
            return result;
        }
    }
}
