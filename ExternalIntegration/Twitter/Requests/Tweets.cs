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
            var auth = Authentication.OAuthAuthentication();
            var response = auth.Tweets.GetTweetAsync(id);

            if(response == null)
            {
                return new DefaultResponse<ITweetsAllData> { Status = 404, Data = null, Message = $"Tweet id {id.ToString()} not found!" };
            }

            var responseAuthor = await auth.UsersV2.GetUserByIdAsync(response.Result.CreatedBy.Id);

            TweetsData tweet = new TweetsData();
            tweet.Id = response.Result.Id;
            tweet.Text = response.Result.Text;
            tweet.Author = responseAuthor.User.Username;
            tweet.CreatedAt = response.Result.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss");
            tweet.Url = response.Result.Url;

            return new DefaultResponse<ITweetsAllData> { Status = HttpStatusCode.OK, Data = tweet, Message = "OK" };
        }

        public static async Task<DefaultResponse<List<ITweetsBasicInformations>>> GetTweetsFromUserByUsername(string username)
        {
            var idUser = Users.GetBaseDataByUsername(username).Result.Data.data.id;

            OAuthRequest oAclient = OAuthRequest.ForProtectedResource("GET", Credentials.ConsumerKey, Credentials.ConsumerKeySecret, Credentials.AccessToken, Credentials.AccessTokenSecret);
            oAclient.RequestUrl = $"https://api.twitter.com/2/users/{idUser}/tweets";
            string auth = oAclient.GetAuthorizationHeader();

            var client = new RestClient("https://api.twitter.com/2/");
            var request = new RestRequest($"users/{idUser}/tweets", Method.Get);
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

            RestResponse response = client.Execute(request);

            var stream = response.Content;

            ITweetsBasicInformations result = JsonConvert.DeserializeObject<TweetsBasicInformations>(stream);

            List<ITweetsBasicInformations> allTweets = new List<ITweetsBasicInformations>();
            allTweets.Add(result);

            if (result.data == null)
            {
                return new DefaultResponse<List<ITweetsBasicInformations>> { Status = 404, Data = null, Message = $"User id {idUser} not found!" };
            }


            if(result.meta.next_token == null)
            {
                return new DefaultResponse<List<ITweetsBasicInformations>> { Status = HttpStatusCode.OK, Data = allTweets, Message = $"OK" };
            }

            while (result.meta.next_token != null)
            {
                oAclient.RequestUrl = $"https://api.twitter.com/2/users/{idUser}/tweets?pagination_token={result.meta.next_token}";
                auth = oAclient.GetAuthorizationHeader();

                var request2 = new RestRequest($"users/{idUser}/tweets?pagination_token={result.meta.next_token}", Method.Get);
                request2.AddHeader("Authorization", auth);
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("Cookie", "guest_id=v1%3A164647635225822612");

                response = client.Execute(request2);

                stream = response.Content;

                result = JsonConvert.DeserializeObject<TweetsBasicInformations>(stream);

                if (result.data != null)
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

        public static async Task<DefaultResponse<ITweetsData>> CreateTweet(string status)
        {
            var auth = Authentication.OAuthAuthentication();
            var response = await auth.Tweets.PublishTweetAsync(status);

            if (response.Text == null)
            {
                return new DefaultResponse<ITweetsData> { Status = 400, Data = null, Message = $"An error ocurred!" };
            }

            var user = await auth.Users.GetAuthenticatedUserAsync();

            TweetsData tweet = new TweetsData();
            tweet.Id = response.Id;
            tweet.Text = response.Text;
            tweet.Url = response.Url;
            tweet.Author = user.ScreenName;
            tweet.CreatedAt = response.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss");

            return new DefaultResponse<ITweetsData> { Status = HttpStatusCode.OK, Data = tweet, Message = $"Your tweet has been published!" };
        }
    }
}
