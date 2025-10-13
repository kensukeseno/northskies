using NorthSkies.Models;
using NorthSkies.Renderers;
using NorthSkies.Services;
using System.Diagnostics;

namespace NorthSkies
{
    public partial class Form1 : Form
    {
        private WeatherService_WeatherAPI _weatherService;
        private ICityRepository _cityManager;
        private WeatherIconRenderer _iconRenderer;
        private List<string> _savedCities;

        public Form1()
        {
            InitializeComponent();

            _weatherService = new WeatherService_WeatherAPI("e4717dbbbff341acb65170334251310");
            _iconRenderer = new WeatherIconRenderer();

            string folder = "Data";
            string fileName = "SavedCities.txt";
            string filePath = Path.Combine(folder, fileName);
            _cityManager = new CityManager(filePath);
            _savedCities = _cityManager.LoadSavedCities();

            _weatherService.GetCurrentWeatherAsync(new City("Calgary","Canada",0.0,0.0)).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.WriteLine($"Error fetching weather data: {task.Exception.Message}");
                    return;
                }
                WeatherData myCurrentWeather = task.Result;
                Debug.WriteLine($"City: Calgary, Temp: {myCurrentWeather.TempC}°C, Condition: {myCurrentWeather.Condition}");
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }
    }
}
