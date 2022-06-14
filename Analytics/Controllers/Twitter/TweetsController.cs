using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Requests;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [Route("[controller]")]
    public class TweetsController : Controller
    {
        [HttpGet]
        [Route("SingleTweet/{id}")]
        public async Task<DefaultResponse<ITweetsAllData>> GetSingleTweetById(long id)
        {
            var response = await Tweets.GetSingleTweetById(id);
            return response;
        }

        [HttpGet]
        [Route("TweetsFromUser/{username}")]
        public async Task<DefaultResponse<List<ITweetsBasicInformations>>> GetTweetsFromUserById(string username)
        {
            var response = await Tweets.GetTweetsFromUserByUsername(username);
            return response;
        }

        [HttpPost]
        [Route("CreateTweet")]
        // POST: CreateTweet
        public async Task<DefaultResponse<ITweetsAllData>> CreateTweet(string status)
        {
            var response = await Tweets.CreateTweet(status);
            return response;
        }
    }
}
