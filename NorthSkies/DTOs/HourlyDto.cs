namespace NorthSkies.DTOs
{
    public class HourlyDto
    {
        public string time { get; set; }
        public double temp_c { get; set; }
        public double temp_f { get; set; }
        public ConditionDto condition { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int humidity { get; set; }
        public double feelslike_c { get; set; }
        public double feelslike_f { get; set; }
    }
}
