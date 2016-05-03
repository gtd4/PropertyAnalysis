using Newtonsoft.Json;

namespace PropertyAnalysisTool.Models
{
    public class Attribute
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}