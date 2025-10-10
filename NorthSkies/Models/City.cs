using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthSkies.Models
{
    internal class City
    {
        public string Name { get; }
        public string Country { get; }
        public double Lat { get; }
        public double Lon { get; }

        public City(string name, string country, double lat, double lon)
        {
            Name = name;
            Country = country;
            Lat = lat;
            Lon = lon;
        }

    }
}
