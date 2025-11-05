using NorthSkies.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Windows.Forms.Design.AxImporter;

namespace NorthSkies.Services
{
    interface ICityManager
    {
        // Load cities that are saved
        public List<City> LoadSavedCities();
        // Add a city to a stored list
        public void AddCity(City city);
        // Remove a city from a stored list
        public void RemoveCity(City city);
        // Search/Autocomplete city using API (This method might need to be moved to WeatherService)
        public string GetCityByName(string name);
        // Load default city
        public City LoadDefaultCity();
        // Set a default city
        public void SetDefaultCity(City city);
    }
}