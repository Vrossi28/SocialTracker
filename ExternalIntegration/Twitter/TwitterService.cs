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
        private static readonly string BEARER = ConfigurationManager.AppSettings["Bearer"];
        private static readonly string CONSUMER_KEY = ConfigurationManager.AppSettings["ConsumerKey"];
        private static readonly string CONSUMER_KEY_SECRET = ConfigurationManager.AppSettings["ConsumerKeySecret"];
        private static readonly string ACCESS_TOKEN = ConfigurationManager.AppSettings["AccessToken"];
        private static readonly string ACCESS_TOKEN_SECRET = ConfigurationManager.AppSettings["AccessTokenSecret"];
        private static readonly string ACCESS_TOKEN = ConfigurationManager.AppSettings["AccessToken"];

        public static Task<HttpResponseMessage> BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER);
        }
    }
}
