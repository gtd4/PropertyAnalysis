using Newtonsoft.Json;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PropertyAnalysisTool.Controllers
{
    public class HomeController : Controller
    {
        string oauthToken = "503E1ECB2CD3D44A98BE089160073C57";
        string oauthSecret = "6994C47CF7D5B369F9702A50D0D81B17";
        string consumerKey = "C92E96B9131B76EF6CC533B1A96D841E";
        string consumerSecret = "7BF2D361B8348A18EF4757E7836B65CD";

        public ActionResult Index(int page = 1)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var response = client.GetAsync(string.Format("https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery&rows=12&page={0}", page)).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);
                    var totalCount = tpr.TotalCount;




                }

            }
            var totalPages = tpr.TotalCount / tpr.PageSize;
            if (tpr.TotalCount % tpr.PageSize != 0)
            {
                totalPages++;
            }

            var model = tpr;
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

        public ActionResult UpdatePropertyListings(int localityId = 0, int districtId = 0, int suburbId = 0, int page = 1)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var url = BuildApiUrl(localityId, districtId, suburbId, page);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);


                }

            }
            var totalPages = tpr.TotalCount / (tpr.PageSize > 0 ? tpr.PageSize : 1);
            if (tpr.PageSize > 1 && tpr.TotalCount % tpr.PageSize != 0)
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

        private string BuildApiUrl(int localityId, int districtId, int suburbId, int page)
        {
            var url = "https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery&rows=12";

            var sb = new StringBuilder(url);

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

            sb.AppendFormat("&page={0}", page);
            return sb.ToString();
        }

        public ActionResult Details(int id)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            var model = new PropertyModel();

            using (var client = new HttpClient())
            {
                InitClient(authHeader, client);

                var response = client.GetAsync("https://api.tmsandbox.co.nz/v1/Listings/" + id + ".json").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    model = JsonConvert.DeserializeObject<PropertyModel>(responseString);

                }
            }
            return View(model);
        }

        public ActionResult Compare(int id1 = 0, int id2 = 0, int id3 = 0)
        {
            var Ids = new int[] { id1, id2, id3};
            //Get upto 3 properties and compare their values side by side
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);
            var model = new ComparePropertyModel();
            using (var client = new HttpClient())
            {

                foreach (var id in Ids)
                {
                    InitClient(authHeader, client);

                    var response = client.GetAsync("https://api.tmsandbox.co.nz/v1/Listings/" + id + ".json").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;

                        var prop = JsonConvert.DeserializeObject<PropertyModel>(responseString);

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