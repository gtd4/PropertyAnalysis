using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.DTOs
{
    public class TradeMePhotoUrlDTO
    {
        [JsonProperty("Thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("Large")]
        public string Large { get; set; }

        [JsonProperty("FullSize")]
        public string FullSize { get; set; }
    }
}