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

        [HttpGet]
        [Route("{username}/BaseData")]
        // GET: BaseData
        public async Task<DefaultResponse<IUserBasicInformations>> GetBaseDataByUsername(string username)
        {
            var response = await Users.GetBaseDataByUsername(username);
            return response;
        }

        [HttpGet]
        [Route("{username}/AllData")]
        // GET: AllInformations
        public async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            var response = await Users.GetAllDataByUsername(username);
            return response;
        }

        [HttpPost]
        [Route("{username}/Follow")]
        // POST: Follow
        public async Task<DefaultResponse<IUser>> Follow(string username)
        {
            var response = await Users.Follow(username);
            return response;
        }

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
