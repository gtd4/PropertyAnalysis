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

        public int PaginationStart
        {
            get
            {
                if(Page <= 5)
                {
                    return 1;
                }

                if(TotalPages - 5 <= Page)
                {
                    return TotalPages - 9;
                }

                return Page - 5;
            }
        }

        public int MaxPagination
        {
            get
            {
                return TotalPages > 10 ? 10 : TotalPages;
            }
        }

        public List<PropertyModel> GetPropertiesCheaperThanRV()
        {
            return Properties.Where(prop => prop.RateableValue != 0 && prop.StartPrice < prop.RateableValue).ToList();
        }

        public TradeMePropertyResultsViewModel()
        {
            Properties = new List<PropertyModel>();
        }
    }
}