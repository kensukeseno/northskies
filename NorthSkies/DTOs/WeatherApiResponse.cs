using System.Collections.Generic;

namespace NorthSkies.DTOs
{
	public class WeatherApiResponse
	{
		public LocationDto location { get; set; }
		public CurrentDto current { get; set; }
		public ForecastDto forecast { get; set; }
	}
}
