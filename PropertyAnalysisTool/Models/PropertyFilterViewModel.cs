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

        public int SelectedLocationId { get; set; }
        public int SelectedDistrictId { get; set; }
        public int SelectedSuburbId { get; set; }
    }
}