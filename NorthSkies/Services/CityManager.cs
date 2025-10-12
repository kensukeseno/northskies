using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NorthSkies.Models;

namespace NorthSkies.Services
{
    interface ICityRepository
    {
        // Load cities that are saved
        public List<string> LoadSavedCities();
        // Add a city to a stored list
        public void AddCity(string city);
        // Remove a city from a stored list
        public void RemoveCity(string city);
        // Search/Autocomplete city using API (This method might need to be moved to WeatherService)
        public string GetCityByName(string name);
    }
    internal class CityManager: ICityRepository
    {
        private readonly string _localFile;

        public CityManager(string localFile)
        {
            _localFile = localFile;
        }

        public List<string> LoadSavedCities() 
        {
            try
            {
                if (File.Exists(_localFile))
                {
                    return new List<string>(File.ReadAllLines(_localFile));
                }
                else
                {
                    File.Create(_localFile).Close();
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
                return new List<string>();
            }
        }
        public void AddCity(string city) {
            try
            {
                if (!File.Exists(_localFile))
                {
                    File.Create(_localFile).Close(); 
                }
                File.AppendAllText(_localFile, city + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
            }
        }
        public void RemoveCity(string city) {
            try
            {
                if (File.Exists(_localFile))
                {
                    List<string> lines = File.ReadAllLines(_localFile).ToList();
                    lines.RemoveAll(line => line == city);
                    File.WriteAllLines(_localFile, lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
            }
        }
        public string GetCityByName(string name) {  return null; }

        
    }
}
