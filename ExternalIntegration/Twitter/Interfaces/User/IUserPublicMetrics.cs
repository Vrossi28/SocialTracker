namespace ExternalIntegration.Twitter.Models
{
    public interface IUserPublicMetrics
    {
        int FollowersCount { get; set; }
        int FollowingCount { get; set; }
        int ListedCount { get; set; }
        int TweetCount { get; set; }
    }
}