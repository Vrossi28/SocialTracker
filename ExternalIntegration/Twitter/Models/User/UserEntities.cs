using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserEntities : IUserEntities
    {
        [JsonProperty(PropertyName = "url")]
        public Url Url { get; set; }
        [JsonProperty(PropertyName = "description")]
        public UserDescription Description { get; set; }
    }

}
