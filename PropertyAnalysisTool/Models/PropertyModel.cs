using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class PropertyModel
    {

        [JsonProperty("ListingId")]
        public int ListingId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("StartPrice")]
        public decimal StartPrice { get; set; }

        [JsonProperty("BuyNowPrice")]
        public decimal BuyNowPrice { get; set; }

        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("IsFeatured")]
        public bool IsFeatured { get; set; }

        [JsonProperty("HasGallery")]
        public bool HasGallery { get; set; }

        [JsonProperty("IsBold")]
        public bool IsBold { get; set; }

        [JsonProperty("IsHighlighted")]
        public bool IsHighlighted { get; set; }

        [JsonProperty("HasHomePageFeature")]
        public bool HasHomePageFeature { get; set; }

        [JsonProperty("MaxBidAmount")]
        public decimal MaxBidAmount { get; set; }

        [JsonProperty("AsAt")]
        public DateTime AsAt { get; set; }

        [JsonProperty("CategroyPath")]
        public string CategroyPath { get; set; }

        [JsonProperty("PictureHref")]
        public string PictureHref { get; set; }

        [JsonProperty("HasPayNow")]
        public bool HasPayNow { get; set; }

        [JsonProperty("IsNew")]
        public bool IsNew { get; set; }

        [JsonProperty("RegionId")]
        public int RegionId { get; set; }

        [JsonProperty("PriceDisplay")]
        public string PriceDisplay { get; set; }

        [JsonProperty("RateableValue")]
        public decimal RateableValue { get; set; }

        [JsonProperty("RentPerWeek")]
        public decimal RentPerWeek { get; set; }

        [JsonProperty("SubTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("Body")]
        public string Description { get; set; }

        [JsonProperty("Photos")]
        public List<PhotoModel> Photos { get; set; }

        [JsonProperty("WasPrice")]
        public decimal WasPrice { get; set; }

        public decimal InitialRent {
            get
            {
                return InitialYieldPercentage / 100 * Price / 52; ;
            }

            set
            {
                InitialRent = value;
            }
            
        }

        public decimal Price
        {
            get
            {
                if (!string.IsNullOrEmpty(new string(this.PriceDisplay.Where(char.IsDigit).ToArray())))
                {
                    return Int32.Parse(new string(this.PriceDisplay.Where(char.IsDigit).ToArray()));
                }
                return 0;
            }
        }

        private decimal _initialYieldPercentage = 10;

        public decimal InitialYieldPercentage
        {
            get
            {
                return _initialYieldPercentage;
            }

            set
            {
                _initialYieldPercentage = value;
            }
        }

    }
}