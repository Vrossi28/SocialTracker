using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Models.Tweets;
using ExternalIntegration.Twitter.Responses;
using Newtonsoft.Json;
using OAuth;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Requests
{
    public static class Tweets
    {
        public static async Task<DefaultResponse<ITweetsAllData>> GetSingleTweetById(long id)
        {
            var auth = Authentication.OAuthTweetInvi();
            var response = await auth.Tweets.GetTweetAsync(id);

            if(response == null)
            {
                return new DefaultResponse<ITweetsAllData> { Status = 404, Data = null, Message = $"Tweet id {id.ToString()} not found!" };
            }

            var responseAuthor = await auth.UsersV2.GetUserByIdAsync(response.CreatedBy.Id);

            ITweetsAllData tweet = new TweetsData();
            tweet.Id = id.ToString();
            tweet.Text = response.Text;
            tweet.Author = responseAuthor.User.Username;
            tweet.CreatedAt = response.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss");
            tweet.Url = response.Url;

            return new DefaultResponse<ITweetsAllData> { Status = HttpStatusCode.OK, Data = tweet, Message = "OK" };
        }

        public static async Task<DefaultResponse<List<ITweetsBasicInformations>>> GetTweetsFromUserByUsername(string username)
        {
            var user = await Users.GetBaseDataByUsername(username);
            if (user.Message != "OK")
            {
                return new DefaultResponse<List<ITweetsBasicInformations>> { Status = user.Status, Data = null, Message = $"{user.Message}" };
            }

            var idUser = user.Data.Data.Id;

            RestResponse response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/tweets", "https://api.twitter.com/2/", 
                $"users/{idUser}/tweets", "GET", Method.Get);

            var stream = response.Content;

            ITweetsBasicInformations result = JsonConvert.DeserializeObject<TweetsBasicInformations>(stream);

            List<ITweetsBasicInformations> allTweets = new List<ITweetsBasicInformations>();
            allTweets.Add(result);

            if (result.Data == null)
            {
                return new DefaultResponse<List<ITweetsBasicInformations>> { Status = 404, Data = null, Message = $"User id {idUser} not found!" };
            }


            if(result.Meta.NextToken == null)
            {
                return new DefaultResponse<List<ITweetsBasicInformations>> { Status = HttpStatusCode.OK, Data = allTweets, Message = $"OK" };
            }

            while (result.Meta.NextToken != null)
            {
                response = Authentication.OAuthAuthentication($"https://api.twitter.com/2/users/{idUser}/tweets?pagination_token={result.Meta.NextToken}", "https://api.twitter.com/2/",
                $"users/{idUser}/tweets?pagination_token={result.Meta.NextToken}", "GET", Method.Get);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<TweetsBasicInformations>(stream);

                if (result.Data != null)
                {
                    allTweets.Add(result);
                }
                else
                {
                    return new DefaultResponse<List<ITweetsBasicInformations>> { Status = HttpStatusCode.OK, Data = allTweets, Message = $"OK" };
                }
            }

            return new DefaultResponse<List<ITweetsBasicInformations>> { Status = HttpStatusCode.OK, Data = allTweets, Message = $"OK" };
        }

        public static async Task<DefaultResponse<ITweetsAllData>> CreateTweet(string status)
        {
            var auth = Authentication.OAuthTweetInvi();
            var response = await auth.Tweets.PublishTweetAsync(status);

            if (response.Text == null)
            {
                return new DefaultResponse<ITweetsAllData> { Status = 400, Data = null, Message = $"An error ocurred!" };
            }

            var user = await auth.Users.GetAuthenticatedUserAsync();

            string[] idTweet = response.Url.Split('/');

            ITweetsAllData tweet = new TweetsData();
            tweet.Id = idTweet[5];
            tweet.Text = response.Text;
            tweet.Url = response.Url;
            tweet.Author = user.ScreenName;
            tweet.CreatedAt = response.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss");

            return new DefaultResponse<ITweetsAllData> { Status = HttpStatusCode.OK, Data = tweet, Message = $"Your tweet has been published!" };
        }

        public static async Task<DefaultResponse<ITweetsBasicInformations>> SearchTweetsByQuote(string quote)
        {
            var httpClient = Authentication.BearerAuthentication();
            var response = await httpClient.GetAsync($"tweets/search/recent?query={quote}");

            if (!response.IsSuccessStatusCode)
            {
                return new DefaultResponse<ITweetsBasicInformations> { Status = response.StatusCode, Data = null, Message = $"Error: {response.ReasonPhrase}" };
            }

            var stream = await response.Content.ReadAsStringAsync();
            var tweets = JsonConvert.DeserializeObject<TweetsBasicInformations>(stream);

            if(tweets.Data.Count == 0)
            {
                return new DefaultResponse<ITweetsBasicInformations> { Status = HttpStatusCode.NoContent, Data = null, Message = $"No tweets found searching by the quote: {quote}" };
            }

            return new DefaultResponse<ITweetsBasicInformations> { Status = HttpStatusCode.OK, Data = tweets, Message = $"Showing the last 10 tweets with the quote: {quote}" };
        }
    }
}
