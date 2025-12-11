using System.Diagnostics;
using NorthSkies.Models;

namespace NorthSkies.Renderers
{
    internal class WeatherIconRenderer
    {
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
