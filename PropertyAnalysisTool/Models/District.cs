﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.Models
{
    public class District : TradeMeLocationModel
    {
        [JsonProperty("DistrictId")]
        public override int Id { get; set; }

        [JsonProperty("Name")]
        public override string Name { get; set; }

        [JsonProperty("Suburbs")]
        public List<Suburb> Suburbs { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}