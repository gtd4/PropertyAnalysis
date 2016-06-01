using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class DetailsViewModel
    {
        public decimal Price { get; set; }
        public decimal InitialYield { get; set; }

        public string Title { get; set; }
    }
}