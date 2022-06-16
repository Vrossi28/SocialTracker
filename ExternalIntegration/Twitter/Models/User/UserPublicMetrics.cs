﻿using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models
{
    public class UserPublicMetrics : IUserPublicMetrics
    {
        [JsonProperty(PropertyName = "followers_count")]
        public int FollowersCount { get; set; }
        [JsonProperty(PropertyName = "following_count")]
        public int FollowingCount { get; set; }
        [JsonProperty(PropertyName = "tweet_count")]
        public int TweetCount { get; set; }
        [JsonProperty(PropertyName = "listed_count")]
        public int ListedCount { get; set; }
    }

}
