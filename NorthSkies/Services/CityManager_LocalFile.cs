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
    internal class CityManager_LocalFile: ICityManager
    {
        private readonly string _localSavedCitiesFile;
        private readonly string _localDefaultCityFile;

        public CityManager_LocalFile(string savedCitiesFile, string defaultCityFile)
        {
            _localSavedCitiesFile = savedCitiesFile;
            _localDefaultCityFile = defaultCityFile;
        }

        public List<City> LoadSavedCities() 
        {
            try
            {
                if (File.Exists(_localSavedCitiesFile))
                {
                    return new List<City>(JsonSerializer.Deserialize<List<City>>(File.ReadAllText(_localSavedCitiesFile)));
                }
                else
                {

                    File.Create(_localSavedCitiesFile).Close();
                    File.WriteAllText(_localSavedCitiesFile, "[{\"Name\": \"Calgary\", \"Country\": \"Canada\"}]");
                    return new List<City>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
                return new List<City>();
            }
        }
        public void AddCity(City city) {
            try
            {
                if (!File.Exists(_localSavedCitiesFile))
                {
                    File.Create(_localSavedCitiesFile).Close(); 
                }
                // Read the file content
                string jsonContent = File.ReadAllText(_localSavedCitiesFile);

                // Deserialize into a list of City objects
                List<City> cities = JsonSerializer.Deserialize<List<City>>(jsonContent);

                if (cities.Find(c => c.Name == city.Name) == null)
                {
                    cities.Add(city);
                }
                string updatedJson = JsonSerializer.Serialize(cities);

                // Write back to file
                File.WriteAllText(_localSavedCitiesFile, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
            }
        }
        public void RemoveCity(City city)
        {
            try
            {
                if (!File.Exists(_localSavedCitiesFile))
                {
                    File.Create(_localSavedCitiesFile).Close();
                }
                // Read the file content
                string jsonContent = File.ReadAllText(_localSavedCitiesFile);

                // Deserialize into a list of City objects
                List<City> cities = JsonSerializer.Deserialize<List<City>>(jsonContent);

                cities.Remove(city);
                string updatedJson = JsonSerializer.Serialize(cities);

                // Write back to file
                File.WriteAllText(_localSavedCitiesFile, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
            }
        }
        public string GetCityByName(string name) {  return null; }

        public City LoadDefaultCity()
        {
            try
            {
                if (!File.Exists(_localDefaultCityFile))
                {
                    File.Create(_localDefaultCityFile).Close();
                }
                // Read the file content
                string jsonContent = File.ReadAllText(_localDefaultCityFile);

                // Deserialize into a list of City objects
                City city = JsonSerializer.Deserialize<City>(jsonContent);

                return city;
            }
            catch (Exception ex) {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
                return null;
            }
            
        }
        // Set a default city
        public void SetDefaultCity(City city)
        {
            try
            {
                if (!File.Exists(_localDefaultCityFile))
                {
                    File.Create(_localDefaultCityFile).Close();
                }
                string cityJson = JsonSerializer.Serialize(city);

                File.WriteAllText(_localDefaultCityFile, cityJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
            }
        }

    }
}
