using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserFollow : IUserFollow
    {
        [JsonProperty(PropertyName = "data")]
        public UserFollowData Data { get; set; }
    }
}
