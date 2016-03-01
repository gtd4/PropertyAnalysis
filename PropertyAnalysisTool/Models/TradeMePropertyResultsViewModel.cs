using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class TradeMePropertyResultsViewModel
    {
        [JsonProperty("List")]
        public List<PropertyModel> Properties { get; set; }

        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int LocalityId { get; set; }
        public int DistrictId { get; set; }
        public int SuburbId { get; set; }
        public int Page { get; set; }

        public List<PropertyModel> GetPropertiesCheaperThanRV()
        {
            return Properties.Where(prop => prop.RateableValue != 0 && prop.StartPrice < prop.RateableValue).ToList();
        }
    }
}