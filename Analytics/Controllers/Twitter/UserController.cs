using Analytics.Configuration;
using ExternalIntegration.Twitter;
using ExternalIntegration.Twitter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        //private Token _token;
        private TwitterService _twitterService = new TwitterService();
        private static readonly string BEARER = "AAAAAAAAAAAAAAAAAAAAALDHdQEAAAAAe6daC47bJsdM4MwYgLDIm8lomNo%3DKgmYYm6C4ZQLACC966jvvHRCwwY92W8700p3aKtDX1qZKYjJLf";
        private static readonly string CONSUMER_KEY = "qmQNsadxMHI061vCIfZPAET2E";
        private static readonly string CONSUMER_KEY_SECRET = "yVURzxZLbAn1uRVz4wytTXojfNrd9IKkvMNd5JOinROqKWI5Vi";
        private static readonly string ACCESS_TOKEN = "3027896394-IE3dQiwgtFxhcNQIStPQswBCVEEY4MayFXiK37O";
        private static readonly string ACCESS_TOKEN_SECRET = "U3kh3aM7yEryec8t27l8v48TbXATv7q3L5bZdGoMH804L";
        private static readonly string CLIENT_ID = "ekkwaEFXdjBJLWFUSjB4R2JCZEw6MTpjaQ";
        private static readonly string CLIENT_ID_SECRET = "TgHXBuLqSoIEMgCSgIVVseuwsjY-4fL2fLxzTd_PNolRxa0Jmw";



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
        [Route("{idUser}/BaseData")]
        // GET: PublicMetrics
        public async Task<IUserBasicInformations> GetBaseData(string idUser)
        {
            HttpClient httpClient = _twitterService.BearerAuthentication(BEARER);
            var response = await httpClient.GetAsync($"users/{idUser}");
            var stream = await response.Content.ReadAsStringAsync();

            IUserBasicInformations result = JsonConvert.DeserializeObject<UserBasicInformations>(stream);

            return result;
        }

        [HttpGet]
        [Route("{idUser}/AllInformations")]
        // GET: PublicMetrics
        public async Task<IUserAllInformations> GetAllInformations(string idUser)
        {
            HttpClient httpClient = _twitterService.BearerAuthentication(BEARER);
            var response = await httpClient
                .GetAsync($"users/{idUser}?user.fields=created_at%2Cpublic_metrics%2Cpinned_tweet_id%2Cprofile_image_url%2Cprotected" +
                $"%2Clocation%2Cdescription%2Centities%2Curl");
            var stream = await response.Content.ReadAsStringAsync();

            IUserAllInformations result = JsonConvert.DeserializeObject<UserAllInformations>(stream);

            return result;
        }
    }
}
