using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class Region : TradeMeLocationModel
    {
        [JsonProperty("LocalityId")]
        public override int Id { get; set; }

        [JsonProperty("Name")]
        public override string Name { get; set; }

        [JsonProperty("Districts")]
        public List<District> Districts { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}