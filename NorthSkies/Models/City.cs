using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthSkies.Models
{
    public class City
    {
        public string Name { get; }
        public string Country { get; }

        public City(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }
}
