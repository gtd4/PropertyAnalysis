using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyAnalysisTool.DTOs
{
    public class TradeMePhotoDTO
    {
        [JsonProperty("Key")]
        public int PhotoId { get; set; }

        [JsonProperty("Value")]
        public PhotoUrl PhotoUrl { get; set; }

        public PhotoModel ToPhotoModel(PhotoModel photoModel)
        {
            photoModel.PhotoId = PhotoId;
            photoModel.PhotoUrl = PhotoUrl;
            return photoModel;
        }
    }
}