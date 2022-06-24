using Newtonsoft.Json;
using System;

namespace ExternalIntegration.Twitter.Models
{
    public class UserAllData : IUserBaseData, IUserAllData
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "public_metrics")]
        public UserPublicMetrics PublicMetrics { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "verified")]
        public bool Verified { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "protected")]
        public bool Protected { get; set; }
        [JsonProperty(PropertyName = "pinned_tweet_id")]
        public string PinnedTweetId { get; set; }
        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty(PropertyName = "entities")]
        public UserEntities Entities { get; set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }
}
