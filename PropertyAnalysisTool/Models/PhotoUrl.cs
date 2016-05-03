using Newtonsoft.Json;

namespace PropertyAnalysisTool.Models
{
    public class PhotoUrl
    {
        [JsonProperty("Thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("Large")]
        public string Large { get; set; }
    }
}