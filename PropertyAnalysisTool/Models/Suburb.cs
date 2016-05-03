using Newtonsoft.Json;
using System.Collections.Generic;

namespace PropertyAnalysisTool.Models
{
    public class Suburb : TradeMeLocationModel
    {
        [JsonProperty("SuburbId")]
        public override int Id { get; set; }

        [JsonProperty("Name")]
        public override string Name { get; set; }

        [JsonProperty("AdjacentSuburbs")]
        List<int> AdjacentSuburbs { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}