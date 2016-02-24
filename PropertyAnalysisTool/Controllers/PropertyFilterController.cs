using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PropertyAnalysisTool.Controllers
{
    public class PropertyFilterController : Controller
    {
        // GET: PropertyFilter
        public ActionResult Index(int localityId = 0, int districtId = 0, int suburbId = 0)
        {
            using (var client = new HttpClient())
            {
                var model = new PropertyFilterViewModel();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "https://api.tmsandbox.co.nz/v1/Localities.json";

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    //To Reduce code and repetition, I tried to abstract region,district, and suburb out to a parent class,
                    //and passed a list of these to a single method rather than having 3 methods that do almost the same thing.
                    //this may use more memory as a 2nd list of the parent object has to be created.

                    //ToDo: Look into how to improve this.
                    var loc = JsonConvert.DeserializeObject<List<Region>>(responseString);

                    model.Locations = GetLocations(loc.ToList<TradeMeLocationModel>(), localityId);

                    var districts = localityId == 0 ? new List<District>() : loc.Where(x => x.Id == localityId).First().Districts;
                    model.Districts = GetLocations(districts.ToList<TradeMeLocationModel>(), districtId);

                    var suburbs = districtId == 0 ? new List<Suburb>() : districts.Where(x => x.Id == districtId).First().Suburbs;
                    model.Suburbs = GetLocations(suburbs.ToList<TradeMeLocationModel>(), suburbId);

                    model.SelectedLocationId = localityId;
                    model.SelectedDistrictId = districtId;
                    model.SelectedSuburbId = suburbId;
                }
                return PartialView("PropertyFilter", model);
            }

        }

        private IEnumerable<SelectListItem> GetLocations(List<TradeMeLocationModel> loc, int id)
        {
            var emptyItem = new Region
            {
                Id = 0,
                Name = "All Regions",

            };

            loc.Add(emptyItem);

            var locations = loc.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == id,
            });

            return locations;
        }

        //private IEnumerable<SelectListItem> GetLocations(List<Region> loc, int localityId)
        //{
        //    var emptyItem = new Region
        //    {
        //        Id = 0,
        //        Name = "All Regions",

        //    };

        //    loc.Add(emptyItem);

        //    var locations = loc.Select(x => new SelectListItem
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Name,
        //        Selected = x.Id == localityId,
        //    });

        //    return locations;
        //}

        //private IEnumerable<SelectListItem> GetDistricts(List<District> loc, int districtId)
        //{

        //    var emptyItem = new District
        //    {
        //        Id = 0,
        //        Name = "All Districts",

        //    };

        //    loc.Add(emptyItem);


        //    var districts = loc.Select(x => new SelectListItem
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Name,
        //        Selected = x.Id == districtId,
        //    });

        //    return districts;
        //}

        //private IEnumerable<SelectListItem> GetSuburbs(List<Suburb> loc)
        //{

        //    var emptyItem = new Suburb
        //    {
        //        Id = 0,
        //        Name = "All Suburb"
        //    };

        //    loc.Add(emptyItem);

        //    var suburbs = loc.Select(x => new SelectListItem
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Name,
        //    });

        //    return suburbs;
        //}
    }
}