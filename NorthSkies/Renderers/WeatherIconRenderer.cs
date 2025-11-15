using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthSkies.Models.Enums;
using NorthSkies.Models;
using System.Drawing;
using System.IO;
using System.Net.Http;

namespace NorthSkies.Renderers
{
    internal class WeatherIconRenderer
    {
        /*private Dictionary<WeatherCondition, Image> _icons;

        public Image GetIcon(WeatherCondition condition)
        {
            return null;
        }
        */


        /* AL - just use WeatherCondition.GetIconUrl
         */
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<Image> RenderIconAsync(WeatherCondition condition, bool isDay)
        {
            string url = "https:" + condition.GetIconUrl(isDay);
            try
            {
                byte[] imageBytes = await _httpClient.GetByteArrayAsync(url);
                using (var ms = new MemoryStream(imageBytes))
                {
                    return new Bitmap(ms);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error downloading icon from {url}: {ex.Message}");
                return SystemIcons.Error.ToBitmap();
            }
            
        }

    }
}
