using CarDeliveryCalculator.DataAccess.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

    public class Coordinates : ICoordinates
    {
        public async Task<Coordinate> GetCoordinatesAsync(City city)
        {
            var apiKey = "a8d744cb1b80bc546f5b63574733f339";
            var link = "http://api.positionstack.com/v1/forward?access_key={0}&query={1}&country={2}";
            var countryCode = await GetCountryCode(city.Country);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format(link, apiKey, city.Name, countryCode))
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

        public async Task<string> GetCountryCode(string countryName) // add to interface !!!
        {
            var strLines = File.ReadLines(@"C:\Projeckts_VS\CarDeliveryCalculator\CarDeliveryCalculator\CarDeliveryCalculator.BusinessLogic\WorkWithCoordinates\CountriesCode.csv");
            foreach (var line in strLines)
            {
                if (line.Split(',')[1].Equals(countryName))
                    return line.Split(',')[0];
            }

            return string.Empty;
        }
    }
}