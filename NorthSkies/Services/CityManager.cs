using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models;

namespace NorthSkies.Services
{
    interface ICityRepository
    {
        // Load cities that are saved
        public List<City> LoadSavedCities();
        // Add a city to a stored list
        public void AddCity(City city);
        // Remove a city from a stored list
        public void RemoveCity(City city);
        // Search/Autocomplete city using API
        public City GetCityByName(string name);
    }
    internal class CityManager: ICityRepository
    {
        public List<City> LoadSavedCities() { return null; }
        public void AddCity(City city) { }
        public void RemoveCity(City city) { }
        public City GetCityByName(string name) {  return null; }
    }
}
