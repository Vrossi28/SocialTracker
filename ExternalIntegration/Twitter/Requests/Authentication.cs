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
using Tweetinvi;
using ExternalIntegration.Twitter.Requests;
using Tweetinvi.Client;

namespace ExternalIntegration.Twitter
{
    public class Authentication
    {

        public static HttpClient BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
            return httpClient;
        }
        public HttpClient BearerAuthentication()
        {
            HttpClient httpClient = BaseApi();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.Bearer);
            return httpClient;
        }

        public static IUsersClient OAuthAuthenticationUsers()
        {
            TwitterClient twitterClient = new TwitterClient(
                Credentials.ConsumerKey,
                Credentials.ConsumerKeySecret,
                Credentials.AccessToken,
                Credentials.AccessTokenSecret
                );
            return twitterClient.Users;
        }
        #region Samples OAuth without TweetInvi
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
        }
        #endregion
    }
}
