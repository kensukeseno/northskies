using System;

namespace NorthSkies.DTOs
{
    public class CityDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string url { get; set; }
    }
}
