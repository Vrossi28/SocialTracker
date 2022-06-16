using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserFollowData
    {
        [JsonProperty(PropertyName = "following")]
        public bool Following { get; set; }
        [JsonProperty(PropertyName = "pending_follow")]
        public bool PendingFollow { get; set; }
    }
}
