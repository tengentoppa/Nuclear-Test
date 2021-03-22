using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AngularMvcTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult AngularTest()
        {
            ViewBag.Message = "Your AngularTest page.";

            ViewBag.wow = "test";
            return View();
        }
        public void test()
        {
            // First create a proxy object
            var proxy = new WebProxy
            {
                Address = new Uri($"http://10.2.0.5:3128"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false
            };

            // Now create a client handler which uses that proxy
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
            };

            HttpClient client = new HttpClient(httpClientHandler);
            client
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent body = new StringContent("{\"MerchantCode\":\"O15o4OBuqBcXsW8BJ6z6swq6beoex45m\",\"PlayerId\":\"IM24TProd12017\",\"Currency\":\"CNY\",\"Password\":\"IM24TProd12017\"}");

            var resultString = client.PostAsync("http://imone.imaegisapi.com/Player/Register", body)
             .Result
             .Content
             .ReadAsStringAsync()
             .Result;
            Response.Write("OK");
            Response.Write(resultString);
        }
    }
}