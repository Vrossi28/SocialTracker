using System;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUserAllData
    {
        bool Protected { get; set; }
        DateTime CreatedAt { get; set; }
        string Description { get; set; }
        UserEntities Entities { get; set; }
        string Location { get; set; }
        string PinnedTweetId { get; set; }
        string ProfileImageUrl { get; set; }
        UserPublicMetrics PublicMetrics { get; set; }
        string Url { get; set; }
        bool Verified { get; set; }
    }
}