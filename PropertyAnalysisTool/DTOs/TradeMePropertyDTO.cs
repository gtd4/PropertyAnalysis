using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.DTOs
{
    public class TradeMePropertyDTO
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
        public DateTime StartDate
        { get; set; }

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

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("Suburb")]
        public string Suburb { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

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
        public List<TradeMePhotoDTO> Photos { get; set; }

        [JsonProperty("WasPrice")]
        public decimal WasPrice { get; set; }

        [JsonProperty("Bedrooms")]
        public string Bedrooms { get; set; }

        [JsonProperty("Bathrooms")]
        public string Bathrooms { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Attributes")]
        public List<Models.Attribute> Attributes { get; set; }



        public TradeMePropertyDTO()
        {
            Attributes = new List<Models.Attribute>();
        }

        public PropertyModel ToPropertyModel(PropertyModel propModel)
        {
            propModel.Address = Address;
            propModel.ListingId = ListingId;
            propModel.Title = Title;
            propModel.Category = Category;
            propModel.StartPrice = StartPrice;
            propModel.BuyNowPrice = BuyNowPrice;
            propModel.StartDate = StartDate;
            propModel.EndDate = EndDate;
            propModel.IsFeatured = IsFeatured;
            propModel.HasGallery = HasGallery;
            propModel.IsBold = IsBold;
            propModel.IsHighlighted = IsHighlighted;
            propModel.HasHomePageFeature = HasHomePageFeature;
            propModel.MaxBidAmount = MaxBidAmount;
            propModel.AsAt = AsAt;
            propModel.CategroyPath = CategroyPath;
            propModel.PictureHref = PictureHref;
            propModel.HasPayNow = HasPayNow;
            propModel.IsNew = IsNew;
            propModel.RegionId = RegionId;
            propModel.Region = Region;
            propModel.Suburb = Suburb;
            propModel.District = District;
            propModel.PriceDisplay = PriceDisplay;
            propModel.RateableValue = RateableValue;
            propModel.RentPerWeek = RentPerWeek;
            propModel.SubTitle = SubTitle;
            propModel.Description = Description;
            propModel.Photos = Photos.Select(x => new PhotoModel
            {
                PhotoId = x.PhotoId,
                PhotoUrl = x.PhotoUrl,
            }).ToList();
            propModel.WasPrice = WasPrice;
            propModel.Bedrooms = Bedrooms;
            propModel.Bathrooms = Bathrooms;
            propModel.Address = Address;
            propModel.Attributes = Attributes;
            return propModel;
        }

        
    }
}