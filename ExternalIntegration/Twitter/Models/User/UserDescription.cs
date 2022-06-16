using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public class UserDescription : IUserDescription
    {
        [JsonProperty(PropertyName = "urls")]
        public List<UrlDetails> Urls { get; set; }
        [JsonProperty(PropertyName = "hashtags")]
        public List<Hashtag> Hashtags { get; set; }
    }

}
