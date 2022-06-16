using System;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using OAuth;
using Tweetinvi;
using ExternalIntegration.Twitter.Requests;

namespace ExternalIntegration.Twitter
{
    public static class Authentication
    {

        public static HttpClient BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
            return httpClient;
        }
        public static HttpClient BearerAuthentication()
        {
            HttpClient httpClient = BaseApi();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.Bearer);
            return httpClient;
        }

        public static TwitterClient OAuthTweetInvi()
        {
            TwitterClient twitterClient = new TwitterClient(
                Credentials.ConsumerKey,
                Credentials.ConsumerKeySecret,
                Credentials.AccessToken,
                Credentials.AccessTokenSecret
                );
            return twitterClient;
        }

        public static RestResponse OAuthAuthentication(string urlAuthentication, string urlBase, string urlRequest, string method, Method methodEnum)
        {
            OAuthRequest oAclient = OAuthRequest.ForProtectedResource(method, Credentials.ConsumerKey, Credentials.ConsumerKeySecret, Credentials.AccessToken, Credentials.AccessTokenSecret);
            oAclient.RequestUrl = urlAuthentication;
            string auth = oAclient.GetAuthorizationHeader();
            
            var client = new RestClient(urlBase);
            var request = new RestRequest(urlRequest, methodEnum);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            RestResponse response = client.Execute(request);

            return response;
        }
        #region Samples OAuth without TweetInvi
        /*
        public static RestRequest OAuthAuthenticationFollow()
        {
            OAuthRequest oAclient = OAuthRequest.ForProtectedResource("POST", Credentials.ConsumerKey, Credentials.ConsumerKeySecret, Credentials.AccessToken, Credentials.AccessTokenSecret);
            oAclient.RequestUrl = $"https://api.twitter.com/2/users/{Credentials.IdUser}/following";
            string auth = oAclient.GetAuthorizationHeader();

            var request = new RestRequest($"users/{Credentials.IdUser}/following", Method.Post);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            return request;
        }

        public static RestRequest OAuthAuthenticationTweet()
        {
            OAuthRequest oAclient = OAuthRequest.ForProtectedResource("POST", Credentials.ConsumerKey, Credentials.ConsumerKeySecret, Credentials.AccessToken, Credentials.AccessTokenSecret);
            oAclient.RequestUrl = "https://api.twitter.com/2/tweets";
            string auth = oAclient.GetAuthorizationHeader();

            var request = new RestRequest("tweets", Method.Post);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            return request; 
        }*/
        #endregion
    }
}
