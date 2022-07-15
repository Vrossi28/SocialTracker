using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Requests;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ExternalIntegration.Twitter.Interfaces.User;
using Microsoft.AspNetCore.Cors;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [EnableCors]
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
        /// Get last tweet from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Returns last tweet from requested username</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("LastTweetFromUser/{username}")]
        [Produces("application/json")]
        public async Task<DefaultResponse<ITweetsBaseData>> GetLastTweetFromUserByUsername(string username)
        {
            var response = await Tweets.GetLastTweetFromUserByUsername(username);
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

        /// <summary>
        /// Search tweets
        /// </summary>
        /// <param name="quote">Quote that should be searched</param>
        /// <response code="200">Success</response>
        /// <response code="204">No content</response>
        /// <response code="401">Not authorized to search quotes</response>
        [HttpGet]
        [Route("SearchTweets/{quote}")]
        [Produces("application/json")]
        // POST: CreateTweet
        public async Task<DefaultResponse<ITweetsBasicInformations>> SearchTweetsByQuote(string quote)
        {
            var response = await Tweets.SearchTweetsByQuote(quote);
            return response;
        }

        /// <summary>
        /// Retweet last 10 tweets by quote
        /// </summary>
        /// <param name="quote">Tweet content that should be published</param>
        /// <response code="200">Tweet successfully created</response>
        /// <response code="401">Not authorized to create a tweet</response>
        [HttpPost]
        [Route("Retweet/{quote}")]
        [Produces("application/json")]
        // POST: CreateTweet
        public async Task<DefaultResponse<List<IRetweetsData>>> RetweetByQuote([Required] string quote)
        {
            var response = await Tweets.RetweetByQuote(quote);
            return response;
        }
    }
}
