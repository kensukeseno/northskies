using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
 