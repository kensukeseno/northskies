using NorthSkies.Models;
using NorthSkies.Renderers;
using NorthSkies.Services;

namespace NorthSkies
{
    public partial class Form1 : Form
    {
        private IWeatherService _weatherService;
        private ICityRepository _cityManager;
        private WeatherIconRenderer _iconRenderer;
        private List<City> _savedCities;
        public Form1()
        {
            InitializeComponent();

            _weatherService = new WeatherService();
            _cityManager = new CityManager();
            _savedCities = _cityManager.LoadSavedCities();
            _iconRenderer = new WeatherIconRenderer();
        }
    }
}
