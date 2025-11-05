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

        private TabControl tabControl;
        private Label lblTemp, lblWind, lblHumidity, lblUnits, lblDefaultCity, lblDefaultCityName, lblSavedCities, lblSearchCity;
        private PictureBox picWeather;
        private TextBox txtBoxCity;
        private TableLayoutPanel forecastPanel;
        private ComboBox cmbUnits, savedCities;
        private Button btnSetDefaullt, btnShowWeather1, btnShowWeather2, btnAddSavedCity;

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
            // Assign the "Current" tab to a tab page
            TabPage currentTab = tabControl.TabPages[0];

            // Design the contents in the "Current" tab
            lblTemp = new Label() { Location = new Point(20, 20), AutoSize = true };
            lblWind = new Label() { Location = new Point(20, 50), AutoSize = true };
            lblHumidity = new Label() { Location = new Point(20, 80), AutoSize = true };

            // Design the weather icon
            picWeather = new PictureBox()
            {
                Location = new Point(400, 20),
                Size = new Size(150, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Add all the designed contents to the tab page
            currentTab.Controls.Add(lblTemp);
            currentTab.Controls.Add(lblWind);
            currentTab.Controls.Add(lblHumidity);
            currentTab.Controls.Add(picWeather);
        }

        private void InitializeForecastTab()
        {
            // Assign the "7-Day Forecast" tab to a tab page
            TabPage forecastTab = tabControl.TabPages[1];

            // Prepare 7 columns for 7-day forecast
            forecastPanel = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = 7,
                Dock = DockStyle.Fill,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            // Set the width to each column
            for (int i = 0; i < 7; i++)
            {
                forecastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
            }

            // Add initial contents to each column
            forecastPanel.Controls.Add(new Label() { Text = "Day", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, 0);
            forecastPanel.Controls.Add(new Label() { Text = "Condition", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 1, 0);
            forecastPanel.Controls.Add(new Label() { Text = "Temp", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 2, 0);

            // Add all the designed contents to the tab page
            forecastTab.Controls.Add(forecastPanel);
        }

        private void InitializeSettingsTab()
        {
            // Assign the "Setting" tab to a tab page
            TabPage settingsTab = tabControl.TabPages[2];

            // Design the contents in the "Setting" tab
            lblUnits = new Label() { Text = "Units:", Location = new Point(20, 20), AutoSize = true };
            cmbUnits = new ComboBox() { Location = new Point(100, 20), Width = 150 };
            cmbUnits.Items.AddRange(new string[] { "Metric (°C, km/h)", "Imperial (°F, mph)" });
            cmbUnits.SelectedIndex = 0;
            lblDefaultCity = new Label() { Text = "Default City: ", Location = new Point(20, 50), AutoSize = true };
            lblDefaultCityName = new Label() { Location = new Point(100, 50), AutoSize = true };
            lblSavedCities = new Label() { Text = "Saved Cities:", Location = new Point(20, 80), AutoSize = true };
            savedCities = new ComboBox() { Location = new Point(100, 80), Width = 150 };
            savedCities.DropDownStyle = ComboBoxStyle.DropDownList;
            btnShowWeather1 = new Button() { Text = "Show Weather", Location = new Point(270, 80), AutoSize = true };
            btnShowWeather1.Click += BtnShowWeather1_Click;
            btnSetDefaullt = new Button() { Text = "Set As Default", Location = new Point(380, 80), AutoSize = true };
            btnSetDefaullt.Click += BtnSetDefault_Click;
            lblSearchCity = new Label() { Text = "Search City:", Location = new Point(20, 110), AutoSize = true };
            txtBoxCity = new TextBox() { Location = new Point(100, 110), AutoSize = true };
            btnShowWeather2 = new Button() { Text = "Show Weather", Location = new Point(270, 110), AutoSize = true };
            btnShowWeather2.Click += BtnShowWeather2_Click;
            btnAddSavedCity = new Button() { Text = "Add to Saved Cities", Location = new Point(380, 110), AutoSize = true };
            btnAddSavedCity.Click += BtnAddSavedCity_Click;


            // Add all the designed contents to the tab page
            settingsTab.Controls.Add(lblUnits);
            settingsTab.Controls.Add(cmbUnits);
            settingsTab.Controls.Add(lblDefaultCity);
            settingsTab.Controls.Add(lblDefaultCityName);
            settingsTab.Controls.Add(lblSavedCities);
            settingsTab.Controls.Add(savedCities);
            settingsTab.Controls.Add(btnShowWeather1);
            settingsTab.Controls.Add(btnSetDefaullt);
            settingsTab.Controls.Add(lblSearchCity);
            settingsTab.Controls.Add(txtBoxCity);
            settingsTab.Controls.Add(btnShowWeather2);
            settingsTab.Controls.Add(btnAddSavedCity);
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
            Text = "NorthSkies";
            ResumeLayout(false);

            // Initialize all the tabs
            InitializeTabs();
            InitializeCurrentWeatherTab();
            InitializeForecastTab();
            InitializeSettingsTab();
        }
    }
}
