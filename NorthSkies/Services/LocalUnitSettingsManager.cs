using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NorthSkies.Models.Enums;

namespace NorthSkies.Services
{
    internal class LocalUnitSettingsManager: IUnitSettingsManager
    {
        private readonly string _localUnitSettingFile;

        public UnitSystem CurrentUnitSystem { get; set; } = UnitSystem.Metric;

        public LocalUnitSettingsManager(string filePath)
        {
            _localUnitSettingFile = filePath;
        }

        public void SaveUnits()
        {
            try
            {
                if (!File.Exists(_localUnitSettingFile))
                {
                    File.Create(_localUnitSettingFile).Close();
                }
                File.WriteAllText(_localUnitSettingFile, CurrentUnitSystem.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving units: {ex.Message}");
            }
        }
      
        public void LoadUnits()
        {
            try
            {
                if (File.Exists(_localUnitSettingFile))
                {
                    if (File.ReadAllText(_localUnitSettingFile).Trim() == "Imperial")
                    {
                        CurrentUnitSystem = UnitSystem.Imperial;
                        return;
                    }
                }
                CurrentUnitSystem = UnitSystem.Metric;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading units: {ex.Message}");
               
                CurrentUnitSystem = UnitSystem.Metric;
            }
        }
    }
}
 