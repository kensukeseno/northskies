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
