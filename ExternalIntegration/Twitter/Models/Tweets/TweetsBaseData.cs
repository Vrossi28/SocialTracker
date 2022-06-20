using ExternalIntegration.Twitter.Interfaces.Tweets;
using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBaseData : ITweetsBaseData
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
