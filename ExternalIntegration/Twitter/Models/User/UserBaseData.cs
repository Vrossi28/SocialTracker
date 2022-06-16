using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserBaseData : IUserBaseData
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
    }

}
