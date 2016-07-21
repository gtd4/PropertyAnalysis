using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;

namespace PropertyAnalysisTool.Controllers
{
    public class HomeController : Controller
    {
        //sandbox
        //string oauthToken = "503E1ECB2CD3D44A98BE089160073C57";
        //string oauthSecret = "6994C47CF7D5B369F9702A50D0D81B17";
        //string consumerKey = "C92E96B9131B76EF6CC533B1A96D841E";
        //string consumerSecret = "7BF2D361B8348A18EF4757E7836B65CD";

        //prod details
        string consumerKey = "48C11C46E3C1969737776DECD5F144B3";

        string consumerSecret = "C841EE5BE675F879097974C6BB163202";
        string oauthToken = "AAF373A7B9ED4157DF12E15F94ECD633";
        string oauthSecret = "C640C7AB6D8DBE8B453721FD14E9525D";

        const int pageSize = 12;

        const string prodEnv = "https://api.trademe.co.nz/v1/";
        const string devEnv = "https://api.tmsandbox.co.nz/v1/";

        public ActionResult Index(int localityId = 0, int districtId = 0, int suburbId = 0, int minBedroom = 0, int maxBedroom = 0, int minBathroom = 0, int maxbathroom = 0, int priceMin = 0, int priceMax = 0, int page = 1, string propType = "")
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var url = BuildApiUrl(localityId, districtId, suburbId, minBedroom, maxBedroom, minBathroom, maxbathroom, priceMin, priceMax, page, propType);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);

                    var totalCount = tpr.TotalCount;

                    foreach (var property in tpr.Properties)
                    {
                        var photo = property.Photos;
                    }
                }
            }
            var totalPages = tpr.TotalCount / pageSize;
            if (tpr.TotalCount % pageSize != 0)
            {
                totalPages++;
            }

            var model = tpr;
            model.PropertyType = propType;
            model.TotalPages = totalPages;
            model.Page = page;

            return View(model);
        }

        public ActionResult CheaperThanRV()
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();
            var ctRVList = new List<PropertyModel>();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var response = client.GetAsync("https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery&rows=500").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);

                    //get how many pages we are going to loop through
                    var totalPages = tpr.TotalCount / tpr.PageSize;
                    if (tpr.TotalCount % tpr.PageSize != 0)
                    {
                        totalPages++;
                    }

                    //get initialListings
                    ctRVList.AddRange(tpr.Properties.Where(x => x.RateableValue > 0));

                    for (int i = 1; i < totalPages; i++)
                    {
                        var url = string.Format("https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery&page={0}&rows=12", i);

                        response = client.GetAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            responseString = response.Content.ReadAsStringAsync().Result;
                            tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);

                            ctRVList.AddRange(tpr.Properties.Where(x => x.RateableValue > 0));
                        }
                    }
                }
            }

            var model = ctRVList.Where(prop => prop.Price > 0 && prop.Price < prop.RateableValue).Distinct().ToList();
            return View("Index", model);
        }

        public ActionResult UpdatePropertyListings(int localityId = 0, int districtId = 0, int suburbId = 0, int minBedroom = 0, int maxBedroom = 0, int minBathroom = 0, int maxbathroom = 0, int priceMin = 0, int priceMax = 0, int page = 1, string propType = "residential")
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var url = BuildApiUrl(localityId, districtId, suburbId, minBedroom, maxBedroom, minBathroom, maxbathroom, priceMin, priceMax, page, propType);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);
                }
            }

            var totalPages = tpr.TotalCount / pageSize;
            if (tpr.TotalCount % pageSize != 0)
            {
                totalPages++;
            }

            var model = tpr;
            model.TotalPages = totalPages;
            model.Page = page;

            return PartialView("PropertyListingPartial", model);
        }

        private static void InitClient(string authHeader, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
        }

        private string BuildApiUrl(int localityId, int districtId, int suburbId, int minBed, int maxBed, int minBath, int maxBath, int priceMin, int priceMax, int page, string propType = "Residential")
        {
            //replace environment in url to switch between sandbox and prod site requests
            var url = string.Format("{0}Search/Property/Residential.json?photo_size=Gallery&rows=12&sort_order=PriceAsc", prodEnv);

            var sb = new StringBuilder(url);

            sb.AppendFormat("&property_type={0}", SetPropertyType(propType));

            if (localityId != 0)
            {
                sb.AppendFormat("&region={0}", localityId);
            }

            if (districtId != 0)
            {
                sb.AppendFormat("&district={0}", districtId);
            }

            if (suburbId != 0)
            {
                sb.AppendFormat("&suburb={0}", suburbId);
            }

            if (minBed != 0)
            {
                sb.AppendFormat("&bedrooms_min={0}", minBed);
            }

            if (maxBed != 0)
            {
                sb.AppendFormat("&bedrooms_max={0}", maxBed);
            }

            if (minBath != 0)
            {
                sb.AppendFormat("&bathrooms_min={0}", minBath);
            }

            if (maxBath != 0)
            {
                sb.AppendFormat("&bathrooms_max={0}", maxBath);
            }

            if (priceMin != 0)
            {
                sb.AppendFormat("&price_min={0}", priceMin);
            }

            if (priceMax != 0)
            {
                sb.AppendFormat("&price_max={0}", priceMax);
            }

            sb.AppendFormat("&page={0}", page);
            return sb.ToString();
        }

        private string SetPropertyType(string propType)
        {
            if(propType.ToLower().Equals("sections"))
            {
                return "Section,Dwelling,BareLand";
            }
            return "Apartment,House,Townhouse,Unit";
        }

        private string BuildApiUrl(int page)
        {
            return BuildApiUrl(0, 0, 0, 0, 0, 0, 0, 0, 0, page);
        }

        public ActionResult Details(int id)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            var model = new PropertyModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);
                model = GetPropertyDetails(id, model, client);
            }

            return View(model);
        }

        private PropertyModel GetPropertyDetails(int id, PropertyModel model, HttpClient client)
        {
            var response = client.GetAsync(string.Format("{0}Listings/{1}.json", prodEnv, id)).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                var tradeMeDto = JsonConvert.DeserializeObject<TradeMeDTO>(responseString);

                model = tradeMeDto.ToPropertyModel(model);

                foreach (var attr in model.Attributes)
                {
                    if (attr.Name.Equals("bedrooms"))
                    {
                        model.Bedrooms = attr.Value;
                        continue;
                    }

                    if (attr.Name.Equals("bathrooms"))
                    {
                        model.Bathrooms = attr.Value;
                    }
                }
            }

            return model;
        }

        public ActionResult Compare(int id1 = 0, int id2 = 0, int id3 = 0)
        {
            var Ids = new int[] { id1, id2, id3 };
            //Get upto 3 properties and compare their values side by side
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);
            var model = new ComparePropertyModel();
            var prop = new PropertyModel();

            using (var client = new HttpClient())
            {
                foreach (var id in Ids)
                {
                    InitClient(authHeader, client);

                    var response = client.GetAsync(string.Format("{0}/Listings/{1}.json", prodEnv, id)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;

                        var tradeMeDTO = JsonConvert.DeserializeObject<TradeMeDTO>(responseString);
                        prop = tradeMeDTO.ToPropertyModel(prop);

                        model.Properties.Add(prop);
                    }
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}