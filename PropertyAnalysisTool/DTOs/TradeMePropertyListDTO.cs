using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.DTOs
{
    public class TradeMePropertyListDTO
    {
        [JsonProperty("List")]
        public List<PropertyModel> Properties { get; set; }

        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        public TradeMePropertyResultsViewModel ToTradeMePropertyResultsViewModel(TradeMePropertyResultsViewModel tpr)
        {
            tpr.Properties = Properties;
            tpr.TotalCount = TotalCount;
            tpr.PageSize = PageSize;

            return tpr;
        }

        public TradeMePropertyResultsViewModel ToTradeMeDealsPropertyResultsViewModel(TradeMePropertyResultsViewModel tpr)
        {
            tpr.Properties = Properties.Where(x => x.RateableValue > 0 && x.Price > 0 && x.Price < x.RateableValue).ToList();
            tpr.TotalCount = TotalCount;
            tpr.PageSize = PageSize;

            return tpr;
        }
    }
}