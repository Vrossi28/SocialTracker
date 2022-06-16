using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserAllInformations : IUserAllInformations
    {
        [JsonProperty(PropertyName = "data")]
        public UserAllData Data { get; set; }
    }
}
