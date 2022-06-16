using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserBasicInformations : IUserBasicInformations
    {
        [JsonProperty(PropertyName = "data")]
        public UserBaseData Data { get; set; }
    }
}
