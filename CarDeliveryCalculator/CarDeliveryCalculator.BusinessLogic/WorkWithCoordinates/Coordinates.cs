using CarDeliveryCalculator.DataAccess.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.WorkWithCoordinates
{
    public class Coordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class AllCoordinates
    {
        public ICollection<Coordinate> data { get; set; }
    }

    public static class Coordinates
    {
        public static async Task<Coordinate> GetCoordinatesAsync(City city)
        {
            var apiKey = "a8d744cb1b80bc546f5b63574733f339";
            var link = "http://api.positionstack.com/v1/forward?access_key={0}&query={1}&country={2}";

            var url = $"{link}";

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format(link, apiKey, "Krakow", "PL"))
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var str = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);
                return JsonConvert.DeserializeObject<AllCoordinates>(body).data.FirstOrDefault();
            }
        }
    }
}