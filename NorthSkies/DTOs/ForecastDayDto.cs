namespace NorthSkies.DTOs
{
    public class ForecastDayDto
    {
        public string date { get; set; }
        public DayDto day { get; set; }
        public List<HourlyDto> hour { get; set; }
    }
}
