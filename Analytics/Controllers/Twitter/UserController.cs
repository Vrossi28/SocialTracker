using ExternalIntegration.Twitter;
using ExternalIntegration.Twitter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Description;
using TweetSharp;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        //private Token _token;
        private TwitterIntegration _twitterIntegration = new TwitterIntegration();
        private TwitterService twitterService = new TwitterService();
        private RestClient client = new RestClient("https://api.twitter.com/2/");

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            /*_token.AccessToken = _configuration.GetSection("Tokens:AcessToken").Value;
            _token.AccessTokenSecret = _configuration.GetSection("Token:AccessTokenSecret").Value;
            _token.Bearer = _configuration.GetSection("Token:Bearer").Value;
            _token.ClientID = _configuration.GetSection("Token:ClientID").Value;
            _token.ClientSecret = _configuration.GetSection("Token:ClientSecret").Value;
            _token.ConsumerKey = _configuration.GetSection("Token:ConsumerKey").Value;
            _token.ConsumerKeySecret = _configuration.GetSection("Token:ConsumerKeySecret").Value;*/
        }

        [HttpGet]
        [Route("{username}/BaseData")]
        // GET: BaseData
        public async Task<IUserBaseData> GetBaseDataByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient.GetAsync($"users/by/username/{username}");
            var stream = await response.Content.ReadAsStringAsync();

            IUserBasicInformations result = JsonConvert.DeserializeObject<UserBasicInformations>(stream);

            return result.data;
        }

        [HttpGet]
        [Route("{username}/AllInformations")]
        // GET: AllInformations
        public async Task<IUserAllData> GetAllInformationsByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient
                .GetAsync($"users/by/username/{username}?user.fields=created_at%2Cpublic_metrics%2Cpinned_tweet_id%2Cprofile_image_url%2Cprotected" +
                $"%2Clocation%2Cdescription%2Centities%2Curl");
            var stream = await response.Content.ReadAsStringAsync();

            IUserAllInformations result = JsonConvert.DeserializeObject<UserAllInformations>(stream);

            return result.data;
        }

        [HttpPost]
        [Route("{username}/Follow")]
        // POST: Follow
        public async Task<UserFollowData> Follow(string username)
        {
            var userData = GetBaseDataByUsername(username);
            var idUser = userData.Result.id;
            var request = TwitterIntegration.OAuthAuthenticationFollow();
            var body = @"{" + "\n" +
            @$"    ""target_user_id"": ""{idUser}""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var stream = response.Content;

            IUserFollow result = JsonConvert.DeserializeObject<UserFollow>(stream);
            return result.data;
        }
    }
}
