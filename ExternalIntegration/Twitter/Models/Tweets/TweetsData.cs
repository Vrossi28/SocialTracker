using ExternalIntegration.Twitter.Interfaces.Tweets;
using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsData : ITweetsAllData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
