using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Models.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBasicInformations : ITweetsBasicInformations
    {
        [JsonProperty(PropertyName = "data")]
        public List<TweetsBaseData> Data { get; set; }
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }
    }
}
