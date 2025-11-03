using NorthSkies.Models;
using NorthSkies.Renderers;
using NorthSkies.Services;

namespace NorthSkies
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private WeatherService_WeatherAPI _weatherService;
        private ICityRepository _cityManager;
        private WeatherIconRenderer _iconRenderer;
        private List<string> _savedCities;

        private TabControl tabControl;
        private Label lblTemp, lblWind, lblHumidity;
        private PictureBox picWeather;
        private TableLayoutPanel forecastPanel;
        private ComboBox cmbUnits;

        private void InitializeTabs()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            tabControl.TabPages.Add("Current");
            tabControl.TabPages.Add("7-Day Forecast");
            tabControl.TabPages.Add("Settings");

            this.Controls.Add(tabControl);
        }

        private void InitializeCurrentWeatherTab()
        {
            TabPage currentTab = tabControl.TabPages[0];

            lblTemp = new Label() { Location = new Point(20, 20), AutoSize = true };
            lblWind = new Label() { Location = new Point(20, 50), AutoSize = true };
            lblHumidity = new Label() { Location = new Point(20, 80), AutoSize = true };

            picWeather = new PictureBox()
            {
                Location = new Point(400, 20),
                Size = new Size(150, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            currentTab.Controls.Add(lblTemp);
            currentTab.Controls.Add(lblWind);
            currentTab.Controls.Add(lblHumidity);
            currentTab.Controls.Add(picWeather);
        }

        private void InitializeForecastTab()
        {
            TabPage forecastTab = tabControl.TabPages[1];

            forecastPanel = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = 7,
                Dock = DockStyle.Fill,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            for (int i = 0; i < 7; i++)
            {
                forecastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
            }

            forecastPanel.Controls.Add(new Label() { Text = "Day", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, 0);
            forecastPanel.Controls.Add(new Label() { Text = "Condition", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 1, 0);
            forecastPanel.Controls.Add(new Label() { Text = "Temp", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 2, 0);

            forecastTab.Controls.Add(forecastPanel);
        }

        private void InitializeSettingsTab()
        {
            TabPage settingsTab = tabControl.TabPages[2];

            Label lblUnits = new Label() { Text = "Units:", Location = new Point(20, 20), AutoSize = true };
            cmbUnits = new ComboBox() { Location = new Point(80, 20), Width = 150 };
            cmbUnits.Items.AddRange(new string[] { "Metric (°C, km/h)", "Imperial (°F, mph)" });
            cmbUnits.SelectedIndex = 0;

            settingsTab.Controls.Add(lblUnits);
            settingsTab.Controls.Add(cmbUnits);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 504);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);

            _weatherService = new WeatherService_WeatherAPI("e4717dbbbff341acb65170334251310");
            _iconRenderer = new WeatherIconRenderer();

            string folder = "Data";
            string fileName = "SavedCities.txt";
            string filePath = Path.Combine(folder, fileName);
            _cityManager = new CityManager(filePath);
            _savedCities = _cityManager.LoadSavedCities();

            InitializeTabs();
            InitializeCurrentWeatherTab();
            InitializeForecastTab();
            InitializeSettingsTab();

            var city = new City("Calgary", "Canada", 0.0, 0.0);
            LoadCurrentWeather(city);
            Load7DayForecast(city);
        }
    }
}
