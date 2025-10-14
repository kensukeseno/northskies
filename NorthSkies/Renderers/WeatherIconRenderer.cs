using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;

namespace NorthSkies.Renderers
{
    internal class WeatherIconRenderer
    {
        /*private Dictionary<WeatherCondition, Image> _icons;

        public Image GetIcon(WeatherCondition condition)
        {
            return null;
        }*/

        public Image GetIcon(int conditionCode, bool isDay)
        {
            string timeOfDay = isDay ? "day" : "night";
            string localPath = $"Assets/Icons/{timeOfDay}_{conditionCode}.png";

            if (File.Exists(localPath))
                return Image.FromFile(localPath);

            string url = $"https://cdn.weatherapi.com/weather/64x64/{timeOfDay}/{conditionCode}.png";
            using var client = new WebClient();
            client.DownloadFile(url, localPath);

            return Image.FromFile(localPath);
        }

    }
}
