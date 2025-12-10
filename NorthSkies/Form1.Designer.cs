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
        private TabPage currentTab, forecastTab, settingsTab, hourlyForecastTab;
        private Label lblTemp, lblWind, lblHumidity, lblUnits, lblDefaultCity, lblDefaultCityName, lblSavedCities, lblSearchCity;
        private PictureBox picWeather;
        private TextBox txtBoxCity;
        private TableLayoutPanel dailyForecastPanel;
        private TableLayoutPanel hourlyForecastPanel;
        private ComboBox cmbUnits, savedCities;
        private Button btnSetDefaulltUnit, btnSetDefaulltCity, btnShowWeather1, btnShowWeather2, btnAddSavedCity;

        private void InitializeTabs()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            tabControl.TabPages.Add("Current");
            tabControl.TabPages.Add("Daily Forecast");
            tabControl.TabPages.Add("Hourly Forecast");
            tabControl.TabPages.Add("Settings");

            this.Controls.Add(tabControl);
        }

        private void InitializeCurrentWeatherTab()
        {
            currentTab = tabControl.TabPages[0];

            // Main container for the tab
            var mainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20),
            };

            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            // LEFT SIDE — TEXT INFO
            var textPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
            };

            lblTemp = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 54, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "--°",
                Margin = new Padding(0, 0, 0, 20),
            };

            lblWind = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Text = "Wind: --",
                Margin = new Padding(0, 5, 0, 5),
            };

            lblHumidity = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Text = "Humidity: --",
                Margin = new Padding(0, 5, 0, 5),
            };

            textPanel.Controls.Add(lblTemp);
            textPanel.Controls.Add(lblWind);
            textPanel.Controls.Add(lblHumidity);

            // RIGHT SIDE — WEATHER ICON
            picWeather = new PictureBox()
            {
                Size = new Size(220, 220),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(40, 0, 0, 0),
                Dock = DockStyle.None
            };

            mainPanel.Controls.Add(textPanel, 0, 0);
            mainPanel.Controls.Add(picWeather, 1, 0);

            currentTab.Controls.Add(mainPanel);
        }

        private void InitializeDailyForecastTab()
        {
            forecastTab = tabControl.TabPages[1];
            Font useFont = new Font("Segoe UI", 32, FontStyle.Bold);

            dailyForecastPanel = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = 3,
                Dock = DockStyle.Top,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Padding = new Padding(10),
            };

            forecastTab.Controls.Add(dailyForecastPanel);
        }

        private void InitializeHourlyForecastTab()
        {
            hourlyForecastTab = tabControl.TabPages[2];

            hourlyForecastPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(10),
            };

            hourlyForecastTab.Controls.Add(hourlyForecastPanel);
        }

        private void InitializeSettingsTab()
        {
            settingsTab = tabControl.TabPages[3];

            var layout = new TableLayoutPanel()
            {
                ColumnCount = 4,
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(20),
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            lblUnits = new Label() { Text = "Units:", AutoSize = true };
            cmbUnits = new ComboBox() { Width = 150 };
            cmbUnits.Items.AddRange(new[] { "Metric (°C, km/h)", "Imperial (°F, mph)" });
            cmbUnits.SelectedIndex = 0;
            btnSetDefaulltUnit = new Button() { Text = "Set Default" };
            btnSetDefaulltUnit.Click += BtnSetDefaultUnit_Click;

            layout.Controls.Add(lblUnits, 0, 0);
            layout.Controls.Add(cmbUnits, 1, 0);
            layout.Controls.Add(btnSetDefaulltUnit, 2, 0);

            lblDefaultCity = new Label() { Text = "Default City:", AutoSize = true };
            lblDefaultCityName = new Label() { AutoSize = true };

            layout.Controls.Add(lblDefaultCity, 0, 1);
            layout.Controls.Add(lblDefaultCityName, 1, 1);

            lblSavedCities = new Label() { Text = "Saved Cities:", AutoSize = true };
            savedCities = new ComboBox() { Width = 150 };
            savedCities.DropDownStyle = ComboBoxStyle.DropDownList;
            btnShowWeather1 = new Button() { Text = "Show Weather" };
            btnShowWeather1.Click += BtnShowWeather1_Click;
            btnSetDefaulltCity = new Button() { Text = "Set Default" };
            btnSetDefaulltCity.Click += BtnSetDefaultCity_Click;

            layout.Controls.Add(lblSavedCities, 0, 2);
            layout.Controls.Add(savedCities, 1, 2);
            layout.Controls.Add(btnShowWeather1, 2, 2);
            layout.Controls.Add(btnSetDefaulltCity, 3, 2);

            lblSearchCity = new Label() { Text = "Search City:", AutoSize = true };
            txtBoxCity = new TextBox() { Width = 150 };
            btnShowWeather2 = new Button() { Text = "Show Weather" };
            btnShowWeather2.Click += BtnShowWeather2_Click;
            btnAddSavedCity = new Button() { Text = "Add" };
            btnAddSavedCity.Click += BtnAddSavedCity_Click;

            layout.Controls.Add(lblSearchCity, 0, 3);
            layout.Controls.Add(txtBoxCity, 1, 3);
            layout.Controls.Add(btnShowWeather2, 2, 3);
            layout.Controls.Add(btnAddSavedCity, 3, 3);

            settingsTab.Controls.Add(layout);
            SetButtonHeights(settingsTab, 32);

        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(876, 504);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "NorthSkies";
            ResumeLayout(false);
        }

        // fix button text cutting off
        private void SetButtonHeights(Control parent, int height)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    btn.Height = height;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = new Padding(0);
                    btn.AutoSize = false;
                }

                if (c.HasChildren)
                {
                    SetButtonHeights(c, height);
                }
            }
        }
    }
}
