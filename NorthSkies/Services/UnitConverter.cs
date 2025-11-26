using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;

namespace NorthSkies.Services
{
    internal class UnitConverter
    {
        public static string GetTemperatureText(double tempC, double tempF, UnitSystem unitSystem)
        {
            if(unitSystem == UnitSystem.Metric)
            {
                return $"{tempC:F0}°C";
            }
            else
            {
                return $"{tempF:F0}°F";
            }
        }
        public static string GetWindSpeedText(double windKPH, double windMPH, UnitSystem unitSystem)
        {
            if (unitSystem == UnitSystem.Metric)
            {
                
                return $"{windKPH:F0} km/h";
            }
            else 
            {
                
                return $"{windMPH:F0} mph";
            }
        }
    }
}
