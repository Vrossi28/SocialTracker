using System;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUserAllData
    {
        bool @protected { get; set; }
        DateTime created_at { get; set; }
        string description { get; set; }
        UserEntities entities { get; set; }
        string location { get; set; }
        string pinned_tweet_id { get; set; }
        string profile_image_url { get; set; }
        UserPublicMetrics public_metrics { get; set; }
        string url { get; set; }
        bool verified { get; set; }
    }
}