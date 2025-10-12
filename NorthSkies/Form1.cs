using NorthSkies.Models;
using NorthSkies.Renderers;
using NorthSkies.Services;
using System.Diagnostics;

namespace NorthSkies
{
    public partial class Form1 : Form
    {
        private IWeatherService _weatherService;
        private ICityRepository _cityManager;
        private WeatherIconRenderer _iconRenderer;
        private List<string> _savedCities;

        public Form1()
        {
            InitializeComponent();

            _weatherService = new WeatherService();
            _iconRenderer = new WeatherIconRenderer();

            string folder = "Data";
            string fileName = "SavedCities.txt";
            string filePath = Path.Combine(folder, fileName);
            _cityManager = new CityManager(filePath);
            _savedCities = _cityManager.LoadSavedCities();

        }
    }
}
