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

namespace PropertyAnalysisTool.Controllers
{
    public class HomeController : Controller
    {
        string oauthToken = "503E1ECB2CD3D44A98BE089160073C57";
        string oauthSecret = "6994C47CF7D5B369F9702A50D0D81B17";
        string consumerKey = "C92E96B9131B76EF6CC533B1A96D841E";
        string consumerSecret = "7BF2D361B8348A18EF4757E7836B65CD";



        public ActionResult Index()
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

                var response = client.GetAsync("https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery").Result;


                if (response.IsSuccessStatusCode)
                {

                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);
                }

               
            }
            var model = tpr.Properties;

            return View(model);
        }

        public ActionResult UpdatePropertyListings(int localityId = 0, int districtId = 0, int suburbId = 0)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            TradeMePropertyResultsViewModel tpr = new TradeMePropertyResultsViewModel();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

                var url = BuildApiUrl(localityId, districtId, suburbId);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    string responseString = response.Content.ReadAsStringAsync().Result;
                    tpr = JsonConvert.DeserializeObject<TradeMePropertyResultsViewModel>(responseString);
                }

            }
            var model = tpr.Properties;
            return PartialView("PropertyListingPartial", model);
        }

        private string BuildApiUrl(int localityId, int districtId, int suburbId)
        {
            var url = "https://api.tmsandbox.co.nz/v1/Search/Property/Residential.json?photo_size=Gallery";

            var sb = new StringBuilder(url);

            if (localityId != 0)
            {
                var regionFilter = string.Format("&region={0}", localityId);
                sb.Append(regionFilter);
            }

            if (districtId != 0)
            {
                var regionFilter = string.Format("&district={0}", districtId);
                sb.Append(regionFilter);
            }

            if (suburbId != 0)
            {
                var regionFilter = string.Format("&suburb={0}", suburbId);
                sb.Append(regionFilter);
            }

            return sb.ToString();
        }

        public ActionResult Details(int id)
        {
            var authHeader = string.Format("oauth_consumer_key={0}, oauth_token={1}, oauth_signature_method=PLAINTEXT, oauth_signature={2}&{3}", consumerKey, oauthToken, consumerSecret, oauthSecret);

            var model = new PropertyModel();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

                

                var response = client.GetAsync("https://api.tmsandbox.co.nz/v1/Listings/" + id + ".json").Result;


                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    model = JsonConvert.DeserializeObject<PropertyModel>(responseString);

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