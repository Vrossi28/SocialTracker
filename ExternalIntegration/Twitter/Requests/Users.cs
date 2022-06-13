using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Tweetinvi.Models;

namespace ExternalIntegration.Twitter.Requests
{
    public static class Users
    {
        private static Authentication _twitterIntegration = new Authentication();
        public static async Task<DefaultResponse<IUserBasicInformations>> GetBaseDataByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient.GetAsync($"users/by/username/{username}");
            if (!response.IsSuccessStatusCode)
            {
                return new DefaultResponse<IUserBasicInformations> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            var stream = await response.Content.ReadAsStringAsync();

            IUserBasicInformations result = JsonConvert.DeserializeObject<UserBasicInformations>(stream);

            if (result.data == null)
            {
                return new DefaultResponse<IUserBasicInformations> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            else
            {
                return new DefaultResponse<IUserBasicInformations> { Status = response.StatusCode, Data = result, Message = "OK" };
            }
        }

        public static async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient
                .GetAsync($"users/by/username/{username}?user.fields=created_at%2Cpublic_metrics%2Cpinned_tweet_id%2Cprofile_image_url%2Cprotected" +
                $"%2Clocation%2Cdescription%2Centities%2Curl");

            if (!response.IsSuccessStatusCode)
            {
                return new DefaultResponse<IUserAllInformations> { Status = response.StatusCode, Data = null, Message = $"An error ocurred: {response.ReasonPhrase}" };
            }

            var stream = await response.Content.ReadAsStringAsync();

            IUserAllInformations result = JsonConvert.DeserializeObject<UserAllInformations>(stream);

            if (result.data == null)
            {
                return new DefaultResponse<IUserAllInformations> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            else
            {
                return new DefaultResponse<IUserAllInformations> { Status = response.StatusCode, Data = result, Message = "OK" };
            }
        }

        public static async Task<DefaultResponse<IUser>> Follow(string username)
        {
            var auth = Authentication.OAuthTweetInvi();
            var response = await auth.Users.FollowUserAsync(username);

            if (response == null)
            {
                return new DefaultResponse<IUser> { Status = HttpStatusCode.NotFound, Data = null, Message = $"User {username} not found!" };
            }

            return new DefaultResponse<IUser> { Status = HttpStatusCode.OK, Data = null, Message = $"Congratulations! You followed {response.ScreenName}!" };
        }

        public static async Task<DefaultResponse<IUserFollow>> Unfollow(string username)
        {
            var auth = Authentication.OAuthTweetInvi();
            var response = await auth.Users.UnfollowUserAsync(username);

            if (response == null)
            {
                return new DefaultResponse<IUserFollow> { Status = HttpStatusCode.NotFound, Data = null, Message = $"User {username} not found!" };
            }
            
            return new DefaultResponse<IUserFollow> { Status = HttpStatusCode.OK, Data = null, Message = $"You unfollowed {response.ScreenName}!" };
        }
    }
}
