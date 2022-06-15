using System.Net.Http;
using System.Threading.Tasks;
using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Responses;
using Newtonsoft.Json;
using System.Net;
using Tweetinvi.Models;
using System.Collections.Generic;
using Tweetinvi.Iterators;
using RestSharp;
using ExternalIntegration.Twitter.Interfaces.User;
using ExternalIntegration.Twitter.Models.User;

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
                return new DefaultResponse<IUserBasicInformations> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ReasonPhrase}" };
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

        public static async Task<DefaultResponse<IUserFollowable>> GetFollowersByUsername(string username)
        {
            var user = await GetBaseDataByUsername(username);
            if (user.Message != "OK")
            {
                return new DefaultResponse<IUserFollowable> { Status = user.Status, Data = null, Message = $"{user.Message}" };
            }

            long idUser = user.Data.data.id;

            RestResponse response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/followers", "https://api.twitter.com/2/", $"users/{idUser}/followers", "GET", Method.Get);

            var stream = response.Content;

            IUserFollowable result = JsonConvert.DeserializeObject<UserFollowable>(stream);

            IUserFollowable allFollowers = new UserFollowable();
            allFollowers.data = result.data;
            int countFollowers = 100;

            if (result.meta == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.BadRequest, Data = null, Message = $"Error" };
            }

            if (result.meta.next_token == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowers, Message = $"OK" };
            }

            while (result.meta.next_token != null)
            {
                response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/followers?pagination_token={result.meta.next_token}", "https://api.twitter.com/2/",
                $"users/{idUser}/followers?pagination_token={result.meta.next_token}", "GET", Method.Get);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<UserFollowable>(stream);

                if (result.data != null)
                {
                    allFollowers.meta = result.meta;
                    countFollowers += result.meta.result_count;
                    foreach (var item in result.data)
                    {
                        allFollowers.meta.result_count = countFollowers;
                        allFollowers.data.Add(item);
                    }
                }
                else
                {
                    return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowers, Message = $"OK" };
                }
            }

            return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowers, Message = $"OK" };
        }

        public static async Task<DefaultResponse<IUserFollowable>> GetFollowingByUsername(string username)
        {
            var user = await GetBaseDataByUsername(username);
            if (user.Message != "OK")
            {
                return new DefaultResponse<IUserFollowable> { Status = user.Status, Data = null, Message = $"{user.Message}" };
            }
            var idUser = user.Data.data.id;
            var response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/following", "https://api.twitter.com/2/", $"users/{idUser}/following", "GET", Method.Get);

            if (!response.StatusDescription.Equals("OK"))
            {
                return new DefaultResponse<IUserFollowable> { Status = response.StatusCode, Data = null, Message = $"Error: {response.StatusDescription}" };
            }

            var stream = response.Content;

            IUserFollowable result = JsonConvert.DeserializeObject<UserFollowable>(stream);

            IUserFollowable allFollowing = new UserFollowable();
            allFollowing.data = result.data;
            int countFollowing = 100;

            if (result.data == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.NotFound, Data = null, Message = $"User {username} not found!" };
            }

            if (result.meta.next_token == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowing, Message = $"OK" };
            }

            while (result.meta.next_token != null)
            {
                response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/following?pagination_token={result.meta.next_token}", "https://api.twitter.com/2/",
                $"users/{idUser}/following?pagination_token={result.meta.next_token}", "GET", Method.Get);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<UserFollowable>(stream);

                if (result.data != null)
                {
                    allFollowing.meta = result.meta;
                    countFollowing += result.meta.result_count;
                    foreach (var item in result.data)
                    {
                        allFollowing.meta.result_count = countFollowing;
                        allFollowing.data.Add(item);
                    }
                }
                else
                {
                    return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowing, Message = $"OK" };
                }
            }

            return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowing, Message = $"OK" };
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
