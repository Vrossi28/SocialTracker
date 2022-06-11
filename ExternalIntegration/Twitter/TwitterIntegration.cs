using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;
using TweetSharp;
using RestSharp;
using OAuth;

namespace ExternalIntegration.Twitter
{
    public class TwitterIntegration
    {
        private static readonly string BEARER = "AAAAAAAAAAAAAAAAAAAAALDHdQEAAAAAe6daC47bJsdM4MwYgLDIm8lomNo%3DKgmYYm6C4ZQLACC966jvvHRCwwY92W8700p3aKtDX1qZKYjJLf";
        private static readonly string CONSUMER_KEY = "NkdJ6PiEF0864EwouJTg1Wgdp";
        private static readonly string CONSUMER_KEY_SECRET = "r9nepnBp69AMm6GJwOibXpneYpCtzEYfUXJf8ZqorwNo6PEiYR";
        private static readonly string ACCESS_TOKEN = "3027896394-MkaJungLXrBEk3NgLFEmr687JUWpnuugpi8B8uD";
        private static readonly string ACCESS_TOKEN_SECRET = "fuC1O8UsaDdbWYee900gOWQeP8ztTNQUmS8TTB1lDUBcF";
        private static readonly string CLIENT_ID = "ekkwaEFXdjBJLWFUSjB4R2JCZEw6MTpjaQ";
        private static readonly string CLIENT_ID_SECRET = "TgHXBuLqSoIEMgCSgIVVseuwsjY-4fL2fLxzTd_PNolRxa0Jmw";
        private static readonly string MY_ID_USER = "3027896394";
        private static TwitterService _twitterService = new TwitterService();

        public static HttpClient BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
            return httpClient;
        }
        public HttpClient BearerAuthentication()
        {
            HttpClient httpClient = BaseApi();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER);
            return httpClient;
        }

        public static RestRequest OAuthAuthenticationFollow()
        {
            OAuthRequest oAclient = OAuthRequest.ForProtectedResource("POST", CONSUMER_KEY, CONSUMER_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            oAclient.RequestUrl = $"https://api.twitter.com/2/users/{MY_ID_USER}/following";
            string auth = oAclient.GetAuthorizationHeader();

            var request = new RestRequest($"users/{MY_ID_USER}/following", Method.Post);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            return request;
        }

        public static RestRequest OAuthAuthenticationTweet()
        {
            OAuthRequest oAclient = OAuthRequest.ForProtectedResource("POST", CONSUMER_KEY, CONSUMER_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            oAclient.RequestUrl = "https://api.twitter.com/2/tweets";
            string auth = oAclient.GetAuthorizationHeader();

            var request = new RestRequest("tweets", Method.Post);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            return request; 
        }

    }
}
