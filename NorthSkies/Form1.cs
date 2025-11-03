using NorthSkies.Models;
using NorthSkies.Renderers;
using NorthSkies.Services;
using System.Diagnostics;

namespace NorthSkies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void LoadCurrentWeather(City city)
        {
            try
            {
                WeatherData weather = await _weatherService.GetCurrentWeatherAsync(city);

                bool isMetric = cmbUnits.SelectedIndex == 0;

                lblTemp.Text = $"Temp: {(isMetric ? weather.TempC : weather.TempF)}°{(isMetric ? "C" : "F")}";
                lblWind.Text = $"Wind: {(isMetric ? weather.WindSpeedKPH : weather.WindSpeedMPH)} {(isMetric ? "km/h" : "mph")}";
                lblHumidity.Text = $"Humidity: {weather.Humidity}%";

                //picWeather.Image = _iconRenderer.RenderIcon(weather.Condition);

                Debug.WriteLine($"City: {city.Name}, Temp: {(isMetric ? weather.TempC : weather.TempF)}°{(isMetric ? "C" : "F")}, Condition: {weather.Condition}");
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
                bool isMetric = cmbUnits.SelectedIndex == 0;

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

                // 1 row, 7 columns
                forecastPanel.RowCount = 1;
                forecastPanel.ColumnCount = 7;
                forecastPanel.Dock = DockStyle.Fill;
                forecastPanel.AutoSize = true;

                for (int i = 0; i < 7; i++)
                    forecastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));

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
                    string tempText = $"{(isMetric ? day.TempC : day.TempF)}°{(isMetric ? "C" : "F")}";

                    Label lblDay = new Label()
                    {
                        Text = dayName,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    PictureBox icon = new PictureBox()
                    {
                        Image = SystemIcons.Information.ToBitmap(), // placeholder icon
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
    }
}
