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
using System.Linq;

namespace ExternalIntegration.Twitter.Requests
{
    public static class Users
    {
        public static async Task<DefaultResponse<IUserBasicInformations>> GetBaseDataByUsername(string username)
        {
            HttpClient httpClient = Authentication.BearerAuthentication();
            var response = await httpClient.GetAsync($"users/by/username/{username}");
            if (!response.IsSuccessStatusCode)
            {
                return new DefaultResponse<IUserBasicInformations> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ReasonPhrase}" };
            }
            var stream = await response.Content.ReadAsStringAsync();

            IUserBasicInformations result = JsonConvert.DeserializeObject<UserBasicInformations>(stream);

            if (result.Data == null)
            {
                return new DefaultResponse<IUserBasicInformations> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ReasonPhrase}" };
            }
            else
            {
                return new DefaultResponse<IUserBasicInformations> { Status = response.StatusCode, Data = result, Message = "OK" };
            }
        }

        public static async Task<DefaultResponse<IUserAllInformations>> GetAllDataByUsername(string username)
        {
            HttpClient httpClient = Authentication.BearerAuthentication();
            var response = await httpClient
                .GetAsync($"users/by/username/{username}?user.fields=created_at%2Cpublic_metrics%2Cpinned_tweet_id%2Cprofile_image_url%2Cprotected" +
                $"%2Clocation%2Cdescription%2Centities%2Curl");

            if (!response.IsSuccessStatusCode)
            {
                return new DefaultResponse<IUserAllInformations> { Status = response.StatusCode, Data = null, Message = $"An error ocurred: {response.ReasonPhrase}" };
            }

            var stream = await response.Content.ReadAsStringAsync();

            IUserAllInformations result = JsonConvert.DeserializeObject<UserAllInformations>(stream);

            if (result.Data == null)
            {
                return new DefaultResponse<IUserAllInformations> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ReasonPhrase}" };
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

            long idUser = user.Data.Data.Id;

            RestResponse response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/followers", "https://api.twitter.com/2/", $"users/{idUser}/followers", "GET", Method.Get);

            var stream = response.Content;

            IUserFollowable result = JsonConvert.DeserializeObject<UserFollowable>(stream);

            IUserFollowable allFollowers = new UserFollowable();
            allFollowers.Data = result.Data;
            allFollowers.Meta = result.Meta;
            int countFollowers = 100;

            if (result.Meta == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = response.StatusCode, Data = null, Message = $"Error {response.ErrorException}" };
            }

            if (result.Meta.NextToken == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowers, Message = $"OK" };
            }

            while (result.Meta.NextToken != null)
            {
                response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/followers?pagination_token={result.Meta.NextToken}", "https://api.twitter.com/2/",
                $"users/{idUser}/followers?pagination_token={result.Meta.NextToken}", "GET", Method.Get);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<UserFollowable>(stream);

                if (result.Data != null)
                {
                    allFollowers.Meta = result.Meta;
                    countFollowers += result.Meta.ResultCount;
                    foreach (var item in result.Data)
                    {
                        allFollowers.Meta.ResultCount = countFollowers;
                        allFollowers.Data.Add(item);
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
            var idUser = user.Data.Data.Id;
            var response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/following", "https://api.twitter.com/2/", $"users/{idUser}/following", "GET", Method.Get);

            if (!response.StatusDescription.Equals("OK"))
            {
                return new DefaultResponse<IUserFollowable> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ErrorException}" };
            }

            var stream = response.Content;

            IUserFollowable result = JsonConvert.DeserializeObject<UserFollowable>(stream);

            IUserFollowable allFollowing = new UserFollowable();
            allFollowing.Data = result.Data;
            allFollowing.Meta = result.Meta;
            int countFollowing = 100;

            if (result.Data == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.NotFound, Data = null, Message = $"User {username} not found!" };
            }

            if (result.Meta.NextToken == null)
            {
                return new DefaultResponse<IUserFollowable> { Status = HttpStatusCode.OK, Data = allFollowing, Message = $"OK" };
            }

            while (result.Meta.NextToken != null)
            {
                response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/following?pagination_token={result.Meta.NextToken}", "https://api.twitter.com/2/",
                $"users/{idUser}/following?pagination_token={result.Meta.NextToken}", "GET", Method.Get);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<UserFollowable>(stream);

                if (result.Data != null)
                {
                    allFollowing.Meta = result.Meta;
                    countFollowing += result.Meta.ResultCount;
                    foreach (var item in result.Data)
                    {
                        allFollowing.Meta.ResultCount = countFollowing;
                        allFollowing.Data.Add(item);
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

        public static async Task<DefaultResponse<List<UserBaseData>>> FollowingDontFollowBack(string username)
        {
            var followersResponse = await GetFollowersByUsername(username);
            if (followersResponse.Data.Data == null)
            {
                return new DefaultResponse<List<UserBaseData>> { Status = followersResponse.Status, Data = null, Message = $"{followersResponse.Message}" };
            }
            var followingResponse = await GetFollowingByUsername(username);

            if (followingResponse.Data.Data == null)
            {
                return new DefaultResponse<List<UserBaseData>> { Status = followingResponse.Status, Data = null, Message = $"{followingResponse.Message}" };
            }

            List<UserBaseData> followingDontFollowBack = followingResponse.Data.Data.Where(i => followersResponse.Data.Data.Select(x => x.Id).Contains(i.Id) == false).ToList();

            return new DefaultResponse<List<UserBaseData>> { Status = followersResponse.Status, Data = followingDontFollowBack, Message = $"Total users that don't follow you back: {followingDontFollowBack.Count()}" };
        }

        public static async Task<DefaultResponse<List<UserBaseData>>> FollowersDontFollowBack(string username)
        {
            var followersResponse = await GetFollowersByUsername(username);
            if (followersResponse.Data.Data == null)
            {
                return new DefaultResponse<List<UserBaseData>> { Status = followersResponse.Status, Data = null, Message = $"{followersResponse.Message}" };
            }
            var followingResponse = await GetFollowingByUsername(username);

            if (followingResponse.Data.Data == null)
            {
                return new DefaultResponse<List<UserBaseData>> { Status = followingResponse.Status, Data = null, Message = $"{followingResponse.Message}" };
            }

            List<UserBaseData> followersDontFollowed = followersResponse.Data.Data.Where(i => followingResponse.Data.Data.Select(x => x.Id).Contains(i.Id) == false).ToList();

            return new DefaultResponse<List<UserBaseData>> { Status = followersResponse.Status, Data = followersDontFollowed, Message = $"Total users that you don't follow back: {followersDontFollowed.Count()}" };
        }
    }
}
