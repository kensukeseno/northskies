using NorthSkies.DTOs;
using NorthSkies.Models;

namespace NorthSkies.Services.Mapping
{
    public interface ICityMapper
    {
        List<City> MapCities(List<CityDto> cities);
    }
}
