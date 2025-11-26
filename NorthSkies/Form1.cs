using NorthSkies.Models;
using NorthSkies.Models.Enums;
using NorthSkies.Renderers;
using NorthSkies.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace NorthSkies
{
    public partial class Form1 : Form
    {
        private WeatherService_WeatherAPI _weatherService;
        private ICityManager _cityManager;
        private WeatherIconRenderer _iconRenderer;
        private List<City> _savedCities;
        private City _defaultCity;

        // Timer to debounce the search city API call
        private System.Windows.Forms.Timer _debounceTimer;
        private const int DebounceDelay = 500; // milliseconds

        // City suggestion dropdown
        private ListBox _suggestionBox;
        private City _selectedCity;

        private string _folder = "Data";
        private string _savedCityFileName = "SavedCities.txt";
        private string _savedCityFilePath;
        private string _defaultCityFileName = "DefaultCity.txt";
        private string _defaultCityFilePath;

        public Form1()
        {
            InitializeComponent();

            _weatherService = new WeatherService_WeatherAPI("e4717dbbbff341acb65170334251310");
            _iconRenderer = new WeatherIconRenderer();
            SettingsManager.LoadUnits();

            cmbUnits.SelectedIndex = SettingsManager.CurrentUnitSystem == UnitSystem.Metric ? 0 : 1;
            // Load and display Saved cities
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
            _savedCityFilePath = Path.Combine(_folder, _savedCityFileName);
            _defaultCityFilePath = Path.Combine(_folder, _defaultCityFileName);
            _cityManager = new CityManager_LocalFile(_savedCityFilePath, _defaultCityFilePath);
            _savedCities = _cityManager.LoadSavedCities();
            ShowSavedCities();

            // Display default city data
            _defaultCity = _cityManager.LoadDefaultCity();
            // if the default city is not set, set calgary as default
            if (_defaultCity == null) {
                _cityManager.SetDefaultCity(new City("Calgary", "Canada"));
                _defaultCity = _cityManager.LoadDefaultCity();
            }
            ShowDefaultCity();

            LoadCurrentWeather(_defaultCity);
            Load7DayForecast(_defaultCity);

            _debounceTimer = new System.Windows.Forms.Timer();
            _debounceTimer.Interval = DebounceDelay;
            _debounceTimer.Tick += DebounceTimer_Tick;

            txtBoxCity.TextChanged += CityTextBox_TextChanged;
            txtBoxCity.KeyDown += TextBoxCity_KeyDown;
            SetupSuggestionBox();
        }

        private void CityTextBox_TextChanged(object sender, EventArgs e)
        {
            // Reset the timer every time the text changes
            _debounceTimer.Stop();
            _debounceTimer.Start();
            // Reset the selected city to null
            _selectedCity = null;
        }

        // Fetch suggested city names after a delay
        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();

            string name = txtBoxCity.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                var cities = await GetCityNames(name);
                List<string> cityNames = cities.Select(c => c.Name).ToList();
                ShowSuggestions(cityNames);
            }
            else
            {
                _suggestionBox.Visible = false;
            }
        }

        private async Task<List<City>> GetCityNames(string name)
        {
            try
            {
                List<City> cities = await _weatherService.GetCityByNameAsync(name);
                return cities;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching cities: {ex.Message}");
                return null;
            }
        }

        private void SetupSuggestionBox()
        {
            _suggestionBox = new ListBox
            {
                Visible = false,
                Height = 100,
                Width = txtBoxCity.Width,
            };
            tabControl.TabPages[2].Controls.Add(_suggestionBox);

            // Place this just below the search city textBox
            _suggestionBox.Left = txtBoxCity.Left;
            _suggestionBox.Top = txtBoxCity.Bottom;

            // Hide when clicking anywhere on TabPage outside textbox or dropdown
            tabControl.TabPages[2].MouseDown += (s, e) =>
            {
                if (!_suggestionBox.Bounds.Contains(e.Location) &&
                    !txtBoxCity.Bounds.Contains(e.Location))
                {
                    _suggestionBox.Visible = false;
                }
            };

            _suggestionBox.Click += SuggestionBox_Click;
        }

        private void ShowSuggestions(List<string> suggestions)
        {
            if (suggestions.Count == 0)
            {
                _suggestionBox.Visible = false;
                return;
            }

            _suggestionBox.Items.Clear();
            _suggestionBox.Items.AddRange(suggestions.ToArray());
            _suggestionBox.Visible = true;
            _suggestionBox.BringToFront();
        }

        private async void SuggestionBox_Click(object sender, EventArgs e)
        {

            if (_suggestionBox.SelectedItem != null)
            {
                string text = _suggestionBox.SelectedItem.ToString();
                txtBoxCity.Text = text;
                _suggestionBox.Visible = false;
                List<City> result = await _weatherService.GetCityByNameAsync(text);
                _selectedCity = result.First();
            }
        }

        private void TextBoxCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && _suggestionBox.Visible)
            {
                _suggestionBox.Focus();
                _suggestionBox.SelectedIndex = 0;
            }
        }

        private async void LoadCurrentWeather(City city)
        {
            try
            {
                WeatherData weather = await _weatherService.GetCurrentWeatherAsync(city);

                //bool isMetric = cmbUnits.SelectedIndex == 0;

                UnitSystem currentUnits = SettingsManager.CurrentUnitSystem;

                lblTemp.Text = UnitConverter.GetTemperatureText(weather.TempC, weather.TempF, currentUnits);
                lblWind.Text = UnitConverter.GetWindSpeedText(weather.WindSpeedKPH, weather.WindSpeedMPH, currentUnits);
                lblHumidity.Text = $"Humidity: {weather.Humidity}%";

                //picWeather.Image = _iconRenderer.RenderIcon(weather.Condition);
                picWeather.Image = await _iconRenderer.RenderIconAsync(weather.Condition, weather.IsDay);

                Debug.WriteLine($"City: {city.Name},Temp: {lblTemp.Text}, Condition Code: {weather.Condition.Code}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching current weather: {ex.Message}");
            }
        }

        private async void Load7DayForecast(City city)
        {
            try
            {
                List<WeatherData> forecast = await _weatherService.GetDailyForecastAsync(city);
                //bool isMetric = cmbUnits.SelectedIndex == 0;

                UnitSystem currentUnits = SettingsManager.CurrentUnitSystem;

                if (forecast == null || forecast.Count == 0)
                {
                    MessageBox.Show("No forecast data available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create or reset
                if (forecastPanel == null)
                {
                    InitializeForecastTab();
                }
                else
                {
                    forecastPanel.Controls.Clear();
                    forecastPanel.ColumnStyles.Clear();
                    forecastPanel.RowStyles.Clear();
                }
                int forecastDays = forecast.Count;
                // 1 row, 7 columns
                forecastPanel.RowCount = 1;
                forecastPanel.ColumnCount = forecastDays;
                forecastPanel.Dock = DockStyle.Fill;
                forecastPanel.AutoSize = true;

                for (int i = 0; i < forecastDays; i++)
                    forecastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / forecastDays));

                int col = 0;
                foreach (var day in forecast)
                {
                    var dayPanel = new TableLayoutPanel()
                    {
                        RowCount = 3,
                        ColumnCount = 1,
                        Dock = DockStyle.Fill,
                        AutoSize = true,
                    };
                    dayPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
                    dayPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 34));
                    dayPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

                    string dayName = day.TimeStamp.ToString("ddd");
                    string tempText = UnitConverter.GetTemperatureText(day.TempC, day.TempF, currentUnits);

                    Label lblDay = new Label()
                    {
                        Text = dayName,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    PictureBox icon = new PictureBox()
                    {
                        Image = await _iconRenderer.RenderIconAsync(day.Condition,true), // placeholder icon
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill
                    };

                    Label lblTemp = new Label()
                    {
                        Text = tempText,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };

                    dayPanel.Controls.Add(lblDay, 0, 0);
                    dayPanel.Controls.Add(icon, 0, 1);
                    dayPanel.Controls.Add(lblTemp, 0, 2);

                    forecastPanel.Controls.Add(dayPanel, col, 0);
                    col++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching 7-day forecast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowSavedCities()
        {
            string[] savedCityNames = _savedCities.Select(c => c.Name).ToArray();
            savedCities.Items.Clear();
            savedCities.Items.AddRange(savedCityNames);
        }

        private void ShowDefaultCity()
        {
            lblDefaultCityName.Text = _defaultCity.Name;
        }

        private void BtnShowWeather1_Click(object sender, EventArgs e)
        {
            string cityName = savedCities.Text;
            if (cityName != "")
            {
                City city = _savedCities.Find(c => c.Name == cityName);
                LoadCurrentWeather(city);
                Load7DayForecast(city);
            }
        }

        private void BtnSetDefault_Click(object sender, EventArgs e)
        {
            string cityName = savedCities.Text;
            if (cityName != "")
            {
                City city = _savedCities.Find(c => c.Name == cityName);
                _cityManager.SetDefaultCity(city);
                _defaultCity = _cityManager.LoadDefaultCity();
                ShowDefaultCity();
            }  
        }

        private void BtnShowWeather2_Click(object sender, EventArgs e)
        {
            if (_selectedCity != null)
            {
                LoadCurrentWeather(_selectedCity);
                Load7DayForecast(_selectedCity);
            }
        }

        private void BtnAddSavedCity_Click(object sender, EventArgs e)
        {
            if (_selectedCity != null)
            {
                _cityManager.AddCity(_selectedCity);
                _savedCities = _cityManager.LoadSavedCities();
                ShowSavedCities();
            }
        }
        private void CmbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbUnits.SelectedIndex == 0)
            {
                SettingsManager.CurrentUnitSystem = UnitSystem.Metric;
            }
            else if (cmbUnits.SelectedIndex == 1)
            {
                SettingsManager.CurrentUnitSystem = UnitSystem.Imperial;
            }
            SettingsManager.SaveUnits();
            LoadCurrentWeather(_defaultCity);
            Load7DayForecast(_defaultCity);
        }
    }
}
