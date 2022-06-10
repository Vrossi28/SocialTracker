using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;

namespace ExternalIntegration.Twitter
{
    public class TwitterService
    {
        public static HttpClient BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
            return httpClient;
        }
        public HttpClient BearerAuthentication(string bearer)
        {
            HttpClient httpClient = BaseApi();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            return httpClient;
        }
    }
}
