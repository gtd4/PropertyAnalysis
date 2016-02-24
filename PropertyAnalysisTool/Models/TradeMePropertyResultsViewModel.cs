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

        public List<PropertyModel> GetPropertiesCheaperThanRV()
        {
            return Properties.Where(prop => prop.RateableValue != 0 && prop.StartPrice < prop.RateableValue).ToList();
        }
    }
}