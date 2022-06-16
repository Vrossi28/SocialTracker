using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models.General
{
    public class Meta
    {
        [JsonProperty(PropertyName = "next_token")]
        public string NextToken { get; set; }
        [JsonProperty(PropertyName = "result_count")]
        public int ResultCount { get; set; }
        [JsonProperty(PropertyName = "newest_id")]
        public string NewestId { get; set; }
        [JsonProperty(PropertyName = "oldest_id")]
        public string OldestId { get; set; }
    }
}
