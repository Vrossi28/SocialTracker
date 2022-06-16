using ExternalIntegration.Twitter.Models.General;
using ExternalIntegration.Twitter.Models.Tweets;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Interfaces.Tweets
{
    public interface ITweetsBasicInformations
    {
        [JsonProperty(PropertyName = "data")]
        List<TweetsBaseData> Data { get; set; }
        [JsonProperty(PropertyName = "meta")]
        Meta Meta { get; set; }
    }
}