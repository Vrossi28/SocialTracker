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

namespace ExternalIntegration.Twitter.Requests
{
    public static class Users
    {
        private static Authentication _twitterIntegration = new Authentication();
        //private TwitterService twitterService = new TwitterService();
        private static RestClient client = new RestClient("https://api.twitter.com/2/");
        public static async Task<DefaultResponse<IUserBasicInformations>> GetBaseDataByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient.GetAsync($"users/by/username/{username}");
            var stream = await response.Content.ReadAsStringAsync();

            IUserBasicInformations result = JsonConvert.DeserializeObject<UserBasicInformations>(stream);

            if (result.data == null)
            {
                return new DefaultResponse<IUserBasicInformations> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            else
            {
                return new DefaultResponse<IUserBasicInformations> { Status = 200, Data = result, Message = "OK" };
            }
        }

        public static async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            HttpClient httpClient = _twitterIntegration.BearerAuthentication();
            var response = await httpClient
                .GetAsync($"users/by/username/{username}?user.fields=created_at%2Cpublic_metrics%2Cpinned_tweet_id%2Cprofile_image_url%2Cprotected" +
                $"%2Clocation%2Cdescription%2Centities%2Curl");
            var stream = await response.Content.ReadAsStringAsync();

            IUserAllInformations result = JsonConvert.DeserializeObject<UserAllInformations>(stream);

            if (result.data == null)
            {
                return new DefaultResponse<IUserAllInformations> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            else
            {
                return new DefaultResponse<IUserAllInformations> { Status = 200, Data = result, Message = "OK" };
            }
        }

        public static async Task<DefaultResponse<IUserFollow>> Follow(string username)
        {
            var userData = await GetBaseDataByUsername(username);
            if (userData.Data == null)
            {
                return new DefaultResponse<IUserFollow> { Status = 404, Data = null, Message = $"User: {username} was not found!" };
            }
            var idUser = userData.Data.data.id;
            var request = Authentication.OAuthAuthenticationFollow();
            var body = @"{" + "\n" +
            @$"    ""target_user_id"": ""{idUser}""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var stream = response.Content;

            IUserFollow result = JsonConvert.DeserializeObject<UserFollow>(stream);
            return new DefaultResponse<IUserFollow> { Status = 200, Data = result, Message = $"Congratulations! Now you are following {username}." };
        }
    }
}
