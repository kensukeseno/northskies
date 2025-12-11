using NorthSkies.Models.Enums;

namespace NorthSkies.Services
{
    interface IUnitSettingsManager
    {
        public UnitSystem CurrentUnitSystem { get; set; }

        public void SaveUnits();

        public void LoadUnits();
    }
}
 