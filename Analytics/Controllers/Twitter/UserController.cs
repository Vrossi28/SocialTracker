using ExternalIntegration.Twitter.Interfaces.User;
using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Requests;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Iterators;
using Tweetinvi.Models;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get basic informations from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/BaseData")]
        [Produces("application/json")]
        // GET: BaseData
        public async Task<DefaultResponse<IUserBasicInformations>> GetBaseDataByUsername(string username)
        {
            var response = await Users.GetBaseDataByUsername(username);
            return response;
        }

        /// <summary>
        /// Get all informations from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/AllData")]
        [Produces("application/json")]
        // GET: AllInformations
        public async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            var response = await Users.GetAllDataByUsername(username);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");

            return response;
        }

        /// <summary>
        /// Get all followers from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/Followers")]
        [Produces("application/json")]
        // GET: Followers
        public async Task<DefaultResponse<IUserFollowable>> GetFollowersByUsername(string username)
        {
            var response = await Users.GetFollowersByUsername(username);
            return response;
        }

        /// <summary>
        /// Get all following from user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/Following")]
        [Produces("application/json")]
        // GET: Followers
        public async Task<DefaultResponse<IUserFollowable>> GetFollowingByUsername(string username)
        {
            var response = await Users.GetFollowingByUsername(username);
            return response;
        }

        /// <summary>
        /// Get a list of users that you follow that don't follow you back
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/Following/DontFollowBack")]
        [Produces("application/json")]
        // GET: Followers
        public async Task<DefaultResponse<List<UserBaseData>>> GetFollowingDontFollowBack(string username)
        {
            var response = await Users.FollowingDontFollowBack(username);
            return response;
        }

        /// <summary>
        /// Get a list of followers that you don't follow back
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Success</response>
        /// <response code="404">Username requested was not found</response>
        [HttpGet]
        [Route("{username}/Followers/DontFollowBack")]
        [Produces("application/json")]
        // GET: Followers
        public async Task<DefaultResponse<List<UserBaseData>>> GetFollowersDontFollowBack(string username)
        {
            var response = await Users.FollowersDontFollowBack(username);
            return response;
        }

        /// <summary>
        /// Follow user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Successfully following the requested username</response>
        /// <response code="404">Username requested was not found</response>
        [HttpPost]
        [Route("{username}/Follow")]
        [Produces("application/json")]
        // POST: Follow
        public async Task<DefaultResponse<IUser>> Follow(string username)
        {
            var response = await Users.Follow(username);
            return response;
        }

        /// <summary>
        /// Unfollow user by username
        /// </summary>
        /// <param name="username">Twitter username</param>
        /// <response code="200">Successfully unfollowed</response>
        /// <response code="404">Username requested was not found</response>
        [HttpDelete]
        [Route("{username}/Unfollow")]
        [Produces("application/json")]
        // DELETE: Unfollow
        public async Task<DefaultResponse<IUserFollow>> Unfollow(string username)
        {
            var response = await Users.Unfollow(username);
            return response;
        }
    }
}
