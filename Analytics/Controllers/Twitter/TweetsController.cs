using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Requests;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [Route("[controller]")]
    public class TweetsController : Controller
    {
        /// <summary>
        /// Get single tweet by id
        /// </summary>
        /// <param name="id">Tweet ID</param>
        /// <response code="200">Returns the requested tweet ID</response>
        /// <response code="404">Tweet ID requested was not found</response>
        [HttpGet]
        [Route("SingleTweet/{id}")]
        [Produces("application/json")]
        public async Task<DefaultResponse<ITweetsAllData>> GetSingleTweetById(long id)
        {
            var response = await Tweets.GetSingleTweetById(id);
            return response;
        }

        /// <summary>
        /// Get all tweets from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Returns all tweets from requested username</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("TweetsFromUser/{username}")]
        [Produces("application/json")]
        public async Task<DefaultResponse<List<ITweetsBasicInformations>>> GetTweetsFromUserById(string username)
        {
            var response = await Tweets.GetTweetsFromUserByUsername(username);
            return response;
        }

        /// <summary>
        /// Create a tweet
        /// </summary>
        /// <param name="status">Tweet content that should be published</param>
        /// <response code="200">Tweet successfully created</response>
        /// <response code="401">Not authorized to create a tweet</response>
        [HttpPost]
        [Route("CreateTweet")]
        [Produces("application/json")]
        // POST: CreateTweet
        public async Task<DefaultResponse<ITweetsAllData>> CreateTweet([Required] string status)
        {
            var response = await Tweets.CreateTweet(status);
            return response;
        }
    }
}
