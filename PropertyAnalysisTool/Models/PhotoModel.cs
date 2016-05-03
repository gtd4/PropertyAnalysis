using Newtonsoft.Json;

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