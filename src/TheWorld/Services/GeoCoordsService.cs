using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        public GeoCoordsService()
        {

        }

        public async Task<GeoCoordsResult> GeoCoordsAsync(string name)
        {
            var result = new GeoCoordsResult()
            {
                Success = false,
                Message = "Failed to get coordinate"
            };

            var apiKey = "At0i3qW6lxpWVHq-H7gJTPR2-5eidlzq-qA_13NPoTByvuETmKx9DobCdUTJ6ct3";
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not found a coords";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if(confidence != "High")
                {
                    result.Message = $"Could not find a confident";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Lat = (double)coords[0];
                    result.Long = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
