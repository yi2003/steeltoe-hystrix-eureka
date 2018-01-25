using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core_hystrix_wrapper_ui.Models;
using System.Net.Http; //for Http calls
using core_hystrix_wrapper_ui.Services; //to get Hystrix protected service call
using Pivotal.Discovery.Client;

namespace core_hystrix_wrapper_ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiscoveryHttpClientHandler _handler;
        private string url = "http://AAAA/api/Recommendations";
        public HomeController(RecommendationService rs, IDiscoveryClient client) {
            this.rs = rs;
            _handler = new DiscoveryHttpClientHandler(client);
        }

        RecommendationService rs;

        public IActionResult Index()
        {
            //call Hystrix-protected service
            List<Recommendations> recommendations = rs.Execute();

            
            //var client = new HttpClient(_handler);
          //  var client = HttpClientFactory.Create(_handler,);

           // var response = client.GetAsync(url).Result;

           // System.Console.WriteLine(recommendations.Count);

            //add results to property bag for view
            ViewData["Recommendations"] = recommendations;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
