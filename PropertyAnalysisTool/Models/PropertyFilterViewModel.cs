using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyAnalysisTool.Models
{
    public class PropertyFilterViewModel
    {
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> Suburbs { get; set; }

        public IEnumerable<SelectListItem> BedroomsMin { get; set; }

        public IEnumerable<SelectListItem> BedroomsMax { get; set; }

        public IEnumerable<SelectListItem> BathroomsMin { get; set; }

        public IEnumerable<SelectListItem> BathroomsMax { get; set; }

        public IEnumerable<SelectListItem> PriceMax { get; set; }

        public IEnumerable<SelectListItem> PriceMin { get; set; }

        public int SelectedLocationId { get; set; }
        public int SelectedDistrictId { get; set; }
        public int SelectedSuburbId { get; set; }

        public int MinBedRoom { get; set; }
        public int MaxBedRoom { get; set; }

        public int MinBathRoom { get; set; }
        public int MaxBathRoom { get; set; }

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}