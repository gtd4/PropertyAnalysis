using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class PhotoModel
    {
        [JsonProperty("Key")]
        public int PhotoId { get; set; }

        [JsonProperty("Value")]
        public PhotoUrl PhotoUrl { get; set; }

    }
}