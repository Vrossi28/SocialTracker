using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Requests;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Analytics.Controllers.Twitter
{
    [ApiController]
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
        // GET: AllInformations
        public async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            var response = await Users.GetAllDataByUsername(username);
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
        // DELETE: Unfollow
        public async Task<DefaultResponse<IUserFollow>> Unfollow(string username)
        {
            var response = await Users.Unfollow(username);
            return response;
        }
    }
}
