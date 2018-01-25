
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.CircuitBreaker.Hystrix;
using System.Net.Http; //for Http calls
using core_hystrix_wrapper_ui.Models;
using Pivotal.Discovery.Client;

namespace core_hystrix_wrapper_ui.Services
{
    //HystrixCommand means no result, HystrixCommand<string> means a string comes back
    public class RecommendationService: HystrixCommand<List<Recommendations>>
    {
        private readonly DiscoveryHttpClientHandler _handler;
        private string url = "http://AAAA/api/Recommendations";

        public RecommendationService(IHystrixCommandOptions options,IDiscoveryClient client) :base(options) {
            //nada     

            _handler = new DiscoveryHttpClientHandler(client);
        }

        protected override List<Recommendations> Run()
        {
            //var client = new HttpClient(_handler);
            //var zipkinHandler = new zipkin4net.Transport.Http.TracingHandler("askorder");

            //var client = HttpClientFactory.Create(_handler, zipkinHandler);   
            //var client = HttpClientFactory.Create(_handler);
            // var client = HttpClientFactory.Create(zipkinHandler);
            var client = new HttpClient(new zipkin4net.Transport.Http.TracingHandler("askorder"));
            var response = client.GetAsync("http://localhost:24944/api/recommendations").Result;


            //var response = client.GetAsync(url).Result;
            var recommendations = response.Content.ReadAsAsync<List<Recommendations>>().Result;

            return recommendations;
        }

        protected override List<Recommendations> RunFallback()
        {
            Recommendations r1 = new Recommendations();
            r1.ProductId = "10007";
            r1.ProductDescription = "Black Hat";
            r1.ProductImage = "https://cdn.shopify.com/s/files/1/0692/5669/products/hatnew_1024x1024.png?v=1458082282";

            List<Recommendations> recommendations = new List<Recommendations>();
            recommendations.Add(r1);

            return recommendations;
        }
    }
}