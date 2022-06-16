using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class Hashtag : IHashtag
    {
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }
        [JsonProperty(PropertyName = "end")]
        public int End { get; set; }
        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }
    }

}
