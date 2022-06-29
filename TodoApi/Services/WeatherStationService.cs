using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
using System;
namespace WeatherApi.Services
{
    public class WeatherStationService
    {
        public WeatherStationService()
        {
        }
        public async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
