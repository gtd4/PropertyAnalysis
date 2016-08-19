using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.DTOs
{
    public class TradeMeRegionDTO: TradeMeLocationModel
    {
        [JsonProperty("LocalityId")]
        public override int Id { get; set; }

        [JsonProperty("Name")]
        public override string Name { get; set; }

        [JsonProperty("Districts")]
        public List<District> Districts { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        public Region ToRegion(Region region)
        {
            region.Count = Count;
            region.Id = Id;
            region.Districts = Districts;
            region.Name = Name;

            return region;

        }
    }
}