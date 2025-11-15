using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthSkies.Models.Enums
{
    internal class SettingsManager
    {
        private static readonly string SettingsFilePath = "Data/UnitSetting.txt";
        public static UnitSystem CurrentUnitSystem { get; set; } = UnitSystem.Metric;

        public static void SaveUnits()
        {
            try
            {
              
                string folder = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                
                File.WriteAllText(SettingsFilePath, CurrentUnitSystem.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving units: {ex.Message}");
            }
        }

       
        public static void LoadUnits()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string unitString = File.ReadAllText(SettingsFilePath).Trim();

                   
                    if (Enum.TryParse(unitString, out UnitSystem loadedSystem))
                    {
                        CurrentUnitSystem = loadedSystem;
                    }
                    else
                    {
                        CurrentUnitSystem = UnitSystem.Metric;
                    }
                }
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading units: {ex.Message}");
               
                CurrentUnitSystem = UnitSystem.Metric;
            }
        }
    }
}
 