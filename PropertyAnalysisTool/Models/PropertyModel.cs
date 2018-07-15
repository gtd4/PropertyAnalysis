using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyAnalysisTool.Models
{
    public class PropertyModel
    {
        public int ListingId { get; set; }

        public int LandArea { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public decimal StartPrice { get; set; }

        public decimal BuyNowPrice { get; set; }

        public DateTime StartDate
        { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsFeatured { get; set; }

        public bool HasGallery { get; set; }

        public bool IsBold { get; set; }

        public bool IsHighlighted { get; set; }

        public bool HasHomePageFeature { get; set; }

        public decimal MaxBidAmount { get; set; }

        public DateTime AsAt { get; set; }

        public string CategroyPath { get; set; }

        public string PictureHref { get; set; }

        public bool HasPayNow { get; set; }

        public bool IsNew { get; set; }

        public int RegionId { get; set; }

        public string Region { get; set; }

        public string Suburb { get; set; }

        public string District { get; set; }

        public string PriceDisplay { get; set; }

        public decimal RateableValue { get; set; }

        public decimal RentPerWeek { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public List<PhotoModel> Photos { get; set; }

        public decimal WasPrice { get; set; }

        public string Bedrooms { get; set; }

        public string Bathrooms { get; set; }

        public string Address { get; set; }

        public List<Attribute> Attributes { get; set; }

        public GeographicLocation GeoLocation { get; set; }

        public string Location
        {
            get
            {
                return Attributes.Any() ? Attributes.Where(x => string.Equals(x.Name.ToLower(), "Location".ToLower())).FirstOrDefault().Value : null;
            }
        }

        public decimal InitialRent
        {
            get
            {
                return Math.Round(InitialYieldPercentage / 100 * Price / (VacancyRate));
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

        public decimal InitialYieldPercentage
        {
            get
            {
                return 10;
            }
        }

        public decimal InitialVacancyRate
        {
            get
            {
                return 2;
            }
        }

        public decimal InitialInterestRate
        {
            get
            {
                return 6.5M;
            }
        }

        public decimal AnnualLoanCost
        {
            get
            {
                return 0;
            }
        }

        public decimal RentToCoverInterest
        {
            get
            {
                return AnnualLoanCost / (VacancyRate);
            }
        }

        public decimal SurplusBeforeExpenses
        {
            get
            {
                return InitialRent - RentToCoverInterest;
            }
        }

        public decimal InitialRates
        {
            get
            {
                return 2000;
            }
        }

        public decimal InitialRepairs
        {
            get
            {
                return 1000;
            }
        }

        public decimal InitialInsurance
        {
            get
            {
                return 1000;
            }
        }

        public decimal PropertyManagementPercentage
        {
            get
            {
                return 8;
            }
        }

        public decimal PropertyManagementAmount
        {
            get
            {
                return InitialRent * (VacancyRate) * (PropertyManagementPercentage / 100);
            }
        }

        public decimal BodyCorpFee
        {
            get
            {
                return 0;
            }
        }

        public decimal TotalInitialExpense
        {
            get
            {
                return PropertyManagementAmount + InitialInsurance + InitialRepairs + InitialRates;
            }
        }

        public decimal RentToCoverMortgageExpenses
        {
            get
            {
                return (TotalInitialExpense + AnnualLoanCost) / VacancyRate;
            }
        }

        public decimal ProposedAnnualRentalIncome
        {
            get
            {
                return (InitialRent) * VacancyRate;
            }
        }

        public decimal VacancyRate
        {
            get
            {
                return 52 - InitialVacancyRate;
            }
        }

        public decimal SurplusAfterExpense
        {
            get
            {
                return InitialRent - RentToCoverMortgageExpenses;
            }
        }

        public string FullAddress
        {
            get
            {
                return string.IsNullOrEmpty(Location) ? string.Format("{0}, {1}, {2}", Address, Suburb, District) : Location;
            }
        }

        public string ListedDate
        {
            get
            {
                return string.Format("{0:dd MMM yy}", StartDate);
            }
        }

        public PropertyModel()
        {
            Attributes = new List<Attribute>();
            PriceDisplay = "0";
        }

        public string TradeMeAddress
        {
            get
            {
                return string.Format("https://www.trademe.co.nz/property/residential-property-for-sale/auction-{0}.htm", this.ListingId);
            }
        }

        //public string QVAddress
        //{
        //    get
        //    {
        //        var address = Location.Split(',');
        //        var street = address[0];

        //        var qvurl = string.Format("https://www.qv.co.nz/search-results/all/{0},{1},{2}", street, Suburb, Region);
        //        return qvurl;
        //    }
        //}
    }
}