using System.Collections.Generic;

namespace PropertyAnalysisTool.Models
{
    public class PropertyFilterModel
    {
        public List<Region> Region { get; set; }
        public List<District> District { get; set; }
        public List<Suburb> Suburb { get; set; }
        public string Keywords { get; set; }
        public string PropertyType { get; set; }
        public int BedroomNumMax { get; set; }
        public int BedroomNumMin { get; set; }
        public int BathroomNumMax { get; set; }
        public int BathroomNumMin { get; set; }
        public decimal PriceMax { get; set; }
        public decimal PriceMin { get; set; }

    }
}