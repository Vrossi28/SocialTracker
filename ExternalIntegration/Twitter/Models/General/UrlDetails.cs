using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UrlDetails : IUrlDetails
    {
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }
        [JsonProperty(PropertyName = "end")]
        public int End { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "expanded_url")]
        public string ExpandedUrl { get; set; }
        [JsonProperty(PropertyName = "display_url")]
        public string DisplayUrl { get; set; }
    }

}
