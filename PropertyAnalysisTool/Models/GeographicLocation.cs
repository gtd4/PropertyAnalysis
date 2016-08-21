using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class GeographicLocation
    {
        [JsonProperty("Latitude")]
        public float Latitude;

        [JsonProperty("Longitude")]
        public float Longitude;
    }
}